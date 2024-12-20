using BjjTrainer.Models.Lessons;
using BjjTrainer.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;

namespace BjjTrainer.Services.Users
{
    public class UserService : ApiService
    {
        private string accessToken;
        private string refreshToken;

        public async Task<string> LoginAsync(string username, string password)
        {
            username = "newuser2";
            password = "securePassword1!";
            var loginModel = new { Username = username, Password = password };
            //var loginModel = new { Username = username, Password = password };
            var response = await HttpClient.PostAsJsonAsync("auth/login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (result != null)
                {
                    SetAuthToken(result.Token);

                    return result.Token;
                }
            }
            throw new Exception("Login failed.");
        }

        public async Task<string> SignupAsync(string username, string email, string password, int schoolId, bool isCoach)
        {
            var signupModel = new
            {
                Username = username,
                Email = email,
                Password = password,
                SchoolId = schoolId,
                IsCoach = isCoach
            };

            var response = await HttpClient.PostAsJsonAsync("auth/signup", signupModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SignupResponse>();
                return result?.Token ?? string.Empty;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                var errorMessage = errorResponse?.Errors != null
                    ? string.Join("\n", errorResponse.Errors)
                    : "Signup failed for unknown reasons.";
                throw new Exception(errorMessage);
            }
        }

        public async Task<List<School>> GetAllSchoolsAsync()
        {
            var response = await HttpClient.GetAsync("school");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<School>>() ?? new List<School>();
            }
            else
            {
                throw new Exception("Failed to retrieve schools.");
            }
        }


        //GET FAVORITES
        public async Task<List<Lesson>> GetUserFavoritesAsync(string userId)
        {
            // Make a GET request to retrieve the user's favorite lessons
            var response = await HttpClient.GetAsync($"users/{userId}/favorites");

            // If the request is successful, deserialize and return the list of lessons
            if (response.IsSuccessStatusCode)
            {
                var favoriteLessons = await response.Content.ReadFromJsonAsync<List<Lesson>>();
                return favoriteLessons ?? new List<Lesson>();
            }
            else
            {
                // Handle error response
                throw new Exception("Failed to retrieve user favorites.");
            }
        }

        //ADD LESSON TO FAVORITES
        public async Task<bool> AddLessonToFavoritesAsync(string userId, int lessonId)
        {
            var response = await HttpClient.PostAsync($"users/{userId}/favorites/{lessonId}", null);
            return response.IsSuccessStatusCode;
        }


        //SET AUTH
        public void SetAuthToken(string token)
        {
            Preferences.Set("AuthToken", token);

            // Extract user ID once from the token and store it
            var userId = GetUserIdFromToken(token);
            if (!string.IsNullOrEmpty(userId))
            {
                Preferences.Set("UserId", userId);
                Preferences.Set("IsLoggedIn", true);

            }
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            try
            {
                var response = await HttpClient.GetAsync($"users/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<User>();
                    return user;
                }
                else
                {
                    // Optionally handle different response statuses
                    System.Diagnostics.Debug.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log them or show alerts)
                System.Diagnostics.Debug.WriteLine($"Exception in GetUserByIdAsync: {ex.Message}");
                return null;
            }
        }

        public static string GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            // Log all claims to inspect the available claim types
            foreach (var claim in jsonToken.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }

            // Look for the "nameidentifier" claim with the URI, which holds the user ID
            var userId = jsonToken.Claims
                .FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            return userId ?? string.Empty;

        }

        public class ErrorResponse
        {
            public List<string> Errors { get; set; }
        }


        public async Task<bool> LogoutAsync()
        {
            Preferences.Remove("AuthToken"); // Remove token from storage
            Preferences.Set("IsLoggedIn", false);
            var response = await HttpClient.PostAsync("auth/logout", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (IsTokenExpired(accessToken))
            {
                var newTokens = await RefreshTokenAsync();
                accessToken = newTokens.AccessToken;
                refreshToken = newTokens.RefreshToken;
            }
            return accessToken;
        }

        private bool IsTokenExpired(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadToken(token) as JwtSecurityToken;
            return jwtToken?.ValidTo < DateTime.UtcNow;
        }

        private async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync()
        {
            var response = await HttpClient.PostAsJsonAsync("api/users/refresh-token", refreshToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<(string AccessToken, string RefreshToken)>();
            }
            else
            {
                throw new Exception("Failed to refresh token");
            }
        }
    }
}

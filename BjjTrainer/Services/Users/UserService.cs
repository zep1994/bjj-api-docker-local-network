using BjjTrainer.Models.Lessons;
using BjjTrainer.Models.Users;
using System.Net.Http.Json;

namespace BjjTrainer.Services.Users
{
    public class UserService : ApiService
    {
        public async Task<string> LoginAsync(string username, string password)
        {
            username = "newuser";
            password = "securePassword1!";
            var loginModel = new { Username = username, Password = password };
            //var loginModel = new { Username = username, Password = password };
            var response = await HttpClient.PostAsJsonAsync("auth/login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (result != null)
                {
                    Preferences.Set("AuthToken", result.Token); // Store token in secure storage
                    return result.Token;
                }
            }
            throw new Exception("Login failed.");
        }

        public async Task<string> SignupAsync(string username, string email, string password)
        {
            var signupModel = new { Username = username, Email = email, Password = password };
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

        public class ErrorResponse
        {
            public List<string> Errors { get; set; }
        }


        public async Task<bool> LogoutAsync()
        {
            Preferences.Remove("AuthToken"); // Remove token from storage
            var response = await HttpClient.PostAsync("auth/logout", null);
            return response.IsSuccessStatusCode;
        }
    }
}

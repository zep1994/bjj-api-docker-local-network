using System.Net.Http.Json;
using BjjTrainer.Models.Lessons;

namespace BjjTrainer.Services.Users
{
    public class UserService : ApiService
    {
        public async Task<string> LoginAsync(string username, string password)
        {
            var loginModel = new { Username = username, Password = password };
            var response = await HttpClient.PostAsJsonAsync("auth/login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result?.Token ?? string.Empty;
            }

            throw new Exception("Login failed.");
        }

        public async Task<bool> SignupAsync(string username, string email, string password)
        {
            var signupModel = new { Username = username, Email = email, Password = password };
            var response = await HttpClient.PostAsJsonAsync("auth/signup", signupModel);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LogoutAsync()
        {
            var response = await HttpClient.PostAsync("auth/logout", null);
            return response.IsSuccessStatusCode;
        }
    }
}

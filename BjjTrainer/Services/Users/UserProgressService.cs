using BjjTrainer.Models.DTO;
using System.Net.Http.Json;

namespace BjjTrainer.Services.Users
{
    public class UserProgressService : ApiService
    {
        public async Task<UserProgressDto> GetUserProgressAsync(string userId)
        {
            var response = await HttpClient.GetFromJsonAsync<UserProgressDto>($"userprogress/{userId}/progress");
            return response ?? new UserProgressDto();
        }
    }
}

using BjjTrainer.Models.Lessons;
using System.Net.Http.Json;

namespace BjjTrainer.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = GetApiBaseUrl()
            };
        }

        // Function to get the correct API base URL depending on the platform
        private Uri GetApiBaseUrl()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                return new Uri("http://10.0.2.2:5057/api/"); // For Android Emulator
            }
            else
            {
                return new Uri("http://localhost:5057/api/"); // Replace with your actual API URL
            }
        }

        public async Task<List<Lesson>> GetLessonsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Lesson>>("lessons");
        }

        public async Task<List<LessonSection>> GetLessonSectionsAsync(int lessonId)
        {
            return await _httpClient.GetFromJsonAsync<List<LessonSection>>($"lessons/{lessonId}/sections");
        }
    }
}

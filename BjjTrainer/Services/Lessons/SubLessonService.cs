using BjjTrainer.Models;
using BjjTrainer.Models.DTO;
using BjjTrainer.Models.Lessons;
using System.Net.Http.Json;
using System.Text.Json;

namespace BjjTrainer.Services.Lessons
{
    public class SubLessonService : ApiService
    {
        public async Task<List<SubLesson>> GetSubLessonsBySectionAsync(int lessonSectionId)
        {
            var response = await HttpClient.GetAsync($"sublessons/sections/{lessonSectionId}");
            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return await response.Content.ReadFromJsonAsync<List<SubLesson>>(options);
        }

        public async Task<SubLessonDetailsDto> GetSubLessonDetailsByIdAsync(int subLessonId)
        {
            var response = await HttpClient.GetAsync($"sublessons/{subLessonId}/details");
            Console.WriteLine($"API Response: {response}");
            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return await response.Content.ReadFromJsonAsync<SubLessonDetailsDto>(options);
        }
    }
}

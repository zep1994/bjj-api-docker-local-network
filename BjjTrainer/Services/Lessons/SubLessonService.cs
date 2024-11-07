using BjjTrainer.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace BjjTrainer.Services.Lessons
{
    public class SubLessonService(int lessonSectionId) : ApiService()
    {
        public async Task<List<SubLesson>> GetSubLessonsBySectionAsync()
        {
            var response = await HttpClient.GetAsync($"sublessons/sections/{lessonSectionId}");
            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            };

            return await response.Content.ReadFromJsonAsync<List<SubLesson>>(options);
        }
    }
}
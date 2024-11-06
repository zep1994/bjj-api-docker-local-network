using BjjTrainer.Models.Lessons;
using System.Net.Http.Json;

namespace BjjTrainer.Services.Lessons
{
    public class LessonService : ApiService
    {
        private readonly HttpClient _httpClient;

        public LessonService() : base() { }

        public async Task<List<Lesson>> GetAllLessons()
        {
            return await HttpClient.GetFromJsonAsync<List<Lesson>>("lessons");
        }

        public async Task<List<LessonSection>> GetLessonSectionsAsync(int lessonId)
        {
            return await HttpClient.GetFromJsonAsync<List<LessonSection>>($"lessons/{lessonId}");
        }
    }
}

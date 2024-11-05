using BjjTrainer.Models.Lessons;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BjjTrainer.Lessons.Services
{
    public class LessonService
    {
        private readonly HttpClient _httpClient;

        public LessonService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5057/") // Set your base address
            };
        }

        public async Task<List<Lesson>> GetAllLessons()
        {
            // Use GetFromJsonAsync to deserialize JSON directly into a List<Lesson>
            var lessons = await _httpClient.GetFromJsonAsync<List<Lesson>>("api/lessons");
            return lessons;
        }
    }
}

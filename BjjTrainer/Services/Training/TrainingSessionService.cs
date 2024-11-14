using BjjTrainer.Models.Training;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BjjTrainer.Services.Training
{
    public class TrainingSessionService : ApiService
    {
        public async Task<List<TrainingSession>> GetAllTrainingSessionsAsync()
        {
            var token = Preferences.Get("AuthToken", string.Empty);
            if (string.IsNullOrEmpty(token))
            {
                // Handle case where token is missing or invalid
                throw new UnauthorizedAccessException("No valid authentication token found.");
            }

            // Add the Authorization header with the token
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            try
            {
                var response = await HttpClient.GetAsync("trainingsessions"); 
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<TrainingSession>>() ?? new List<TrainingSession>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Handle 401 Unauthorized error specifically
                    throw new UnauthorizedAccessException("Unauthorized access - please check the token.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching training sessions: {ex.Message}");
            }
            return new List<TrainingSession>();
        }

        public async Task<TrainingSession> CreateTrainingSessionAsync(TrainingSession session)
        {
            var response = await HttpClient.PostAsJsonAsync("trainingsessions", session);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TrainingSession>();
        }

        public async Task<TrainingSession> GetTrainingSessionByIdAsync(int id)
        {
            var response = await HttpClient.GetFromJsonAsync<TrainingSession>($"trainingsessions/{id}");
            return response;
        }
    }
}

using BjjTrainer.Models.DTO;
using BjjTrainer.Models.TrainingGoal;
using System.Net.Http.Json;

namespace BjjTrainer.Services.TrainingGoals
{
    public class TrainingGoalService : ApiService
    {
        public async Task<bool> CreateTrainingGoalAsync(CreateTrainingGoalDto dto)
        {
            try
            {
                var response = await HttpClient.PostAsJsonAsync("traininggoal/create", dto);

                if (!response.IsSuccessStatusCode)
                {
                    var errorDetails = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API error: {response.StatusCode} - {errorDetails}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create training goal: {ex.Message}");
            }
        }


        public async Task<List<TrainingGoal>> GetTrainingGoalsAsync(string userId)
        {
            try
            {
                var goals = await HttpClient.GetFromJsonAsync<List<TrainingGoal>>($"traininggoal/{userId}");
                return goals ?? new List<TrainingGoal>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch training goals: {ex.Message}");
            }
        }
    }
}
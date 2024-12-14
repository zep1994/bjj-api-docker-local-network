using BjjTrainer.Models.DTO;
using BjjTrainer.Models.TrainingGoal;
using System.Net.Http.Json;

namespace BjjTrainer.Services.TrainingGoals
{
    public class TrainingGoalService : ApiService
    {
        public string userId { get; private set; }

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


        public async Task<List<TrainingGoal>> GetTrainingGoalsAsync()
        {
            try
            {
                var userId = Preferences.Get("UserId", string.Empty);
                if (string.IsNullOrEmpty(userId))
                {
                    Console.WriteLine("User ID is missing in Preferences.");
                    return new List<TrainingGoal>();
                }

                var url = $"TrainingGoal/{userId}";
                Console.WriteLine($"Fetching goals from URL: {url}");

                var response = await HttpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var goals = await response.Content.ReadFromJsonAsync<List<TrainingGoal>>();
                if (goals == null || !goals.Any())
                {
                    Console.WriteLine("No goals found.");
                    return new List<TrainingGoal>();
                }

                foreach (var goal in goals)
                {
                    Console.WriteLine($"Goal: {goal.Notes}, Moves Count: {goal.UserTrainingGoalMoves?.Count ?? 0}");
                }

                return goals;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching training goals: {ex.Message}");
                return new List<TrainingGoal>();
            }
        }

    }
}
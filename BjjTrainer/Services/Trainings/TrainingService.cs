using BjjTrainer.Models.TrainingGoal;
using BjjTrainer.Views.Moves;
using System.Text;
using System.Text.Json;

namespace BjjTrainer.Services.Trainings
{
    public class TrainingService : ApiService
    {
        public async Task<List<TopMovesModel>> GetTopMovesAsync()
        {
            try
            {
                // Mocking data for now, replace with API or DB calld
                var userId = Preferences.Get("UserId", string.Empty);
                var topMoves = new List<TopMovesModel>
                {
                    new TopMovesModel { MoveName = "Armbar", UsagePercentage = 25.0 },
                    new TopMovesModel { MoveName = "Triangle Choke", UsagePercentage = 20.0 },
                    new TopMovesModel { MoveName = "Rear Naked Choke", UsagePercentage = 15.0 }
                };

                return await Task.FromResult(topMoves);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching top moves: {ex.Message}");
            }
        }

        public async Task<TrainingSummaryModel> GetTrainingSummaryAsync()
        {
            try
            {
                // Mocking training summary data for now
                var userId = Preferences.Get("UserId", string.Empty);

                // Replace this block with actual API call
                var trainingSummary = new TrainingSummaryModel
                {
                    TotalTrainingLogs = 50,
                    TotalTrainingTime = "25 hours",
                    AverageTrainingTime = "30 minutes"
                };

                return await Task.FromResult(trainingSummary);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching training summary: {ex.Message}");
            }
        }


        public async Task<bool> SubmitTrainingLogAsync(object trainingLog)
        {
            try
            {
                // Serialize the training log object to JSON
                var json = JsonSerializer.Serialize(trainingLog);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // POST request to the API endpoint
                var response = await HttpClient.PostAsync("traininglog/log", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                // Extract error message if the submission fails
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Server error: {errorMessage}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to submit training log: {ex.Message}");
            }
        }
    }
}

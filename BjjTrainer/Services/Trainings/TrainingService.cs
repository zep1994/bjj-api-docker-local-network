using BjjTrainer.Models.DTO;
using BjjTrainer.Models.DTO.TrainingLog;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BjjTrainer.Services.Trainings
{
    public class TrainingService : ApiService
    {
        // GET ALL BY UserId
        public async Task<List<TrainingLogDto>> GetTrainingLogsAsync(string userId)
        {
            try
            {
                var response = await HttpClient.GetFromJsonAsync<List<TrainingLogDto>>($"traininglog/list/{userId}");
                return response ?? new List<TrainingLogDto>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching training logs: {ex.Message}");
            }
        }

        // GET BY UserId
        public async Task<TrainingLogDto> GetTrainingLogByIdAsync(int logId)
        {
            try
            {
                return await HttpClient.GetFromJsonAsync<TrainingLogDto>($"traininglog/{logId}")
                    ?? throw new Exception("Training log not found.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching training log: {ex.Message}");
            }
        }

        // CREATE
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

        // PUT
        public async Task UpdateTrainingLogAsync(int logId, UpdateTrainingLogDto updatedLog)
        {
            try
            {
                var response = await HttpClient.PutAsJsonAsync($"traininglog/{logId}", updatedLog);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error updating training log: {error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating training log: {ex.Message}");
            }
        }

        // DELETE
        public async Task DeleteTrainingLogAsync(int logId)
        {
            try
            {
                var response = await HttpClient.DeleteAsync($"traininglog/{logId}");
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error deleting training log: {error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting training log: {ex.Message}");
            }
        }
    }
}

using System.Text;
using System.Text.Json;

namespace BjjTrainer.Services.Training
{
    public class TrainingService : ApiService
    {
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

using BjjTrainer.Models.Training;
using System.Diagnostics;
using System.Net.Http;
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

        public async Task<TrainingSession> CreateTrainingSessionAsync(TrainingSession trainingSession)
        {
            try
            {
                // Retrieve the stored token
                var authToken = Preferences.Get("AuthToken", string.Empty);

                if (string.IsNullOrEmpty(authToken))
                {
                    Debug.WriteLine("Authorization token is missing.");
                    throw new UnauthorizedAccessException("User is not authenticated. Authorization token is missing.");
                }

                // Set the Authorization header
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

//_________________________________________________________________________DEBUGGGING___________________________________________________________________________________________________
                // Log the request details
                Debug.WriteLine("Sending Create Training Session request...");
                Debug.WriteLine("Request URL: " + HttpClient.BaseAddress + "trainingsessions");
                Debug.WriteLine("Headers: ");
                foreach (var header in HttpClient.DefaultRequestHeaders)
                {
                    Debug.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
                }

                // Convert the request payload to JSON for logging
                var trainingSessionJson = System.Text.Json.JsonSerializer.Serialize(trainingSession);
                Debug.WriteLine("Request Payload: " + trainingSessionJson);
//_________________________________________________________________________DEBUGGGING___________________________________________________________________________________________________

                // Send the POST request
                var response = await HttpClient.PostAsJsonAsync("trainingsessions", trainingSession);

                // Log the response
                var responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Response Status: {response.StatusCode}");
                Debug.WriteLine("Response Content: " + responseContent);

                if (response.IsSuccessStatusCode)
                {
                    var createdSession = await response.Content.ReadFromJsonAsync<TrainingSession>();
                    return createdSession;
                }
                else
                {
                    throw new Exception($"Failed to create session. Status: {response.StatusCode}");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error: {ex.Message}");
            }
            return trainingSession;
        }

        public async Task<TrainingSession> GetTrainingSessionByIdAsync(int id)
        {
            var response = await HttpClient.GetFromJsonAsync<TrainingSession>($"trainingsessions/{id}");
            return response;
        }
    }
}

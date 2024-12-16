using BjjTrainer.Models.DTO.Events;
using System.Text;
using System.Text.Json;

namespace BjjTrainer.Services.Events
{
    public class EventService : ApiService
    {
        public EventService() : base() { }

        public async Task<bool> CreateEventAsync(CalendarEventCreateDTO newEvent)
        {
            try
            {
                // Serialize the event data
                var json = JsonSerializer.Serialize(newEvent);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send POST request to the API
                var response = await HttpClient.PostAsync("calendar", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Server error: {errorMessage}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create event: {ex.Message}");
            }
        }

        public async Task<CalendarEventDto> GetEventByIdAsync(int eventId)
        {
            try
            {
                // Send GET request to fetch event by ID
                var response = await HttpClient.GetAsync($"calendar/{eventId}");

                if (response.IsSuccessStatusCode)
                {
                    var eventJson = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<CalendarEventDto>(eventJson);
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Server error: {errorMessage}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch event: {ex.Message}");
            }
        }

        public async Task<bool> UpdateEventAsync(int eventId, CalendarEventDto updatedEvent)
        {
            try
            {
                // Serialize updated event data
                var json = JsonSerializer.Serialize(updatedEvent);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send PUT request to update the event
                var response = await HttpClient.PutAsync($"calendar/{eventId}", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Server error: {errorMessage}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update event: {ex.Message}");
            }
        }

        public async Task<bool> DeleteEventAsync(int eventId)
        {
            try
            {
                // Send DELETE request to delete the event
                var response = await HttpClient.DeleteAsync($"calendar/{eventId}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Server error: {errorMessage}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete event: {ex.Message}");
            }
        }

        public async Task<List<CalendarEventDto>> GetAllUserEventsAsync(string userId)
        {
            try
            {
                var response = await HttpClient.GetAsync($"calendar/user/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Raw JSON Response: {jsonResponse}");
                    try
                    {
                        return JsonSerializer.Deserialize<List<CalendarEventDto>>(jsonResponse);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Deserialization error: {ex.Message}");
                        return new List<CalendarEventDto>();
                    }
                }

                return new List<CalendarEventDto>(); // Return an empty list on error
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve events: {ex.Message}");
            }
        }
    }
}
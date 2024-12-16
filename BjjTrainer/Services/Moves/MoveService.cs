using BjjTrainer.Models.Moves;
using System.Net.Http.Json;

namespace BjjTrainer.Services.Moves
{
    public class MoveService : ApiService
    {
        public async Task<List<Move>> GetAllMovesAsync()
        {
            try
            {
                return await HttpClient.GetFromJsonAsync<List<Move>>("moves") ?? new List<Move>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching moves: {ex.Message}");
            }
        }
    }
}

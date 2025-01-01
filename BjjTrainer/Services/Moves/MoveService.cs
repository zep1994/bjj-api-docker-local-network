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
                var moves = await HttpClient.GetFromJsonAsync<List<Move>>("moves");
                return moves ?? new List<Move>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching moves: {ex.Message}");
                return new List<Move>();
            }
        }
    }
}

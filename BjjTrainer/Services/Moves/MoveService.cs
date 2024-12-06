using BjjTrainer.Models.Move;
using BjjTrainer.ViewModels.Moves;
using System.Text.Json;

namespace BjjTrainer.Services.Moves
{
    public class MoveService : ApiService
    {
        public async Task<List<MoveViewModel>> GetMovesAsync()
        {
            try
            {
                var response = await HttpClient.GetAsync("moves");
                var responseContent = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                // Deserialize API response into a list of MoveViewModel
                var moves = JsonSerializer.Deserialize<List<MoveResponse>>(json);

                return moves.Select(move => new MoveViewModel
                {
                    Id = move.Id,
                    Name = move.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to fetch moves", ex);
            }
        }
    }
}

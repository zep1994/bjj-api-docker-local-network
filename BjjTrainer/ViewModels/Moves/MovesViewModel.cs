using BjjTrainer.Models.Move;
using BjjTrainer.Services.Moves;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BjjTrainer.ViewModels.Moves
{
    public class MovesViewModel : BaseViewModel
    {
        private readonly MoveService _moveService;

        public ObservableCollection<Move> Moves { get; set; } = new ObservableCollection<Move>();

        public Command LoadMovesCommand { get; }

        public MovesViewModel()
        {
            _moveService = new MoveService();
            LoadMovesCommand = new Command(async () => await LoadMovesAsync());
        }

        public async Task LoadMovesAsync()
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                var moves = await _moveService.GetAllMovesAsync();
                Moves.Clear();
                foreach (var move in moves)
                {
                    Moves.Add(move);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading moves: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
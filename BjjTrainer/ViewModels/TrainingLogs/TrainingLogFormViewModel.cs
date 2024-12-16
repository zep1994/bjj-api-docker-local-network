using System.Collections.ObjectModel;
using BjjTrainer.Models.Move;
using BjjTrainer.Models.DTO;
using BjjTrainer.Services.Moves;
using BjjTrainer.Services.Trainings;
using MvvmHelpers;

namespace BjjTrainer.ViewModels.TrainingGoals
{
    public class TrainingLogFormViewModel : BaseViewModel
    {
        private readonly TrainingService _trainingService;
        private readonly MoveService _moveService;

        public ObservableCollection<Move> Moves { get; private set; } = new ObservableCollection<Move>();

        public DateTime Date { get; set; } = DateTime.Now;
        public double? TrainingTime { get; set; }
        public int? RoundsRolled { get; set; }
        public int? Submissions { get; set; }
        public int? Taps { get; set; }
        public string? Notes { get; set; }

        public TrainingLogFormViewModel()
        {
            _trainingService = new TrainingService();
            _moveService = new MoveService();
            LoadMovesAsync();
        }

        private async void LoadMovesAsync()
        {
            try
            {
                var moves = await _moveService.GetAllMovesAsync();
                Moves.Clear();
                foreach (var move in moves)
                {
                    move.IsSelected = false;
                    Moves.Add(move);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading moves: {ex.Message}");
            }
        }

        public async Task<bool> SubmitLogAsync()
        {
            IsBusy = true;

            try
            {
                string userId = Preferences.Get("UserId", string.Empty);

                if (string.IsNullOrEmpty(userId))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "User not authenticated. Please log in.", "OK");
                    return false;
                }

                var selectedMoves = Moves.Where(m => m.IsSelected).Select(m => m.Id).ToList();
                if (!selectedMoves.Any())
                {
                    await App.Current.MainPage.DisplayAlert("Message", "Are you sure you did not train any moves?", "OK");
                }

                var trainingLog = new TrainingLogDto
                {
                    ApplicationUserId = userId,
                    Date = Date,
                    TrainingTime = TrainingTime ?? 0,
                    RoundsRolled = RoundsRolled ?? 0,
                    Submissions = Submissions ?? 0,
                    Taps = Taps ?? 0,
                    Notes = Notes ?? "",
                    MoveIds = selectedMoves ?? []
                };

                var success = await _trainingService.SubmitTrainingLogAsync(trainingLog);

                if (success)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "Training log submitted successfully!", "OK");
                    return true;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Failed to submit training log: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }

            return false;
        }
    }
}

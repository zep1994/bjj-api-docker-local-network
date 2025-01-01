using BjjTrainer.Models.DTO.TrainingLog;
using BjjTrainer.Models.Moves;
using BjjTrainer.Services.Moves;
using BjjTrainer.Services.Trainings;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace BjjTrainer.ViewModels.TrainingLogs
{
    public class UpdateTrainingLogViewModel : BaseViewModel
    {
        private readonly TrainingService _trainingService;
        private readonly MoveService _moveService;
        private readonly int _logId;

        public ObservableCollection<Move> Moves { get; private set; } = [];
        public List<int> MoveIds { get; private set; } = [];  // FIX: Declare MoveIds

        public DateTime Date { get; set; }
        public double TrainingTime { get; set; }
        public int RoundsRolled { get; set; }
        public int Submissions { get; set; }
        public int Taps { get; set; }
        public string? Notes { get; set; }
        public string? SelfAssessment { get; set; }
        public bool IsCoachLog { get; set; }

        private string _selectedDateDisplay;
        public string SelectedDateDisplay
        {
            get => _selectedDateDisplay;
            set
            {
                _selectedDateDisplay = value;
                OnPropertyChanged();
            }
        }

        public UpdateTrainingLogViewModel(int logId)
        {
            _trainingService = new TrainingService();
            _moveService = new MoveService();
            _logId = logId;

            LoadLogDetails();
        }

        private async void LoadLogDetails()
        {
            IsBusy = true;

            try
            {
                var log = await _trainingService.GetTrainingLogByIdAsync(_logId);
                if (log != null)
                {
                    Date = log.Date;
                    SelectedDateDisplay = log.Date.ToString("yyyy-MM-dd");
                    TrainingTime = log.TrainingTime;
                    RoundsRolled = log.RoundsRolled;
                    Submissions = log.Submissions;
                    Taps = log.Taps;
                    Notes = log.Notes ?? string.Empty;
                    SelfAssessment = log.SelfAssessment;
                    IsCoachLog = log.IsCoachLog;
                    MoveIds = log.MoveIds;

                    Moves.Clear();

                    // Load all available moves and handle empty scenarios
                    var allMoves = await _moveService.GetAllMovesAsync();
                    if (allMoves == null || !allMoves.Any())
                    {
                        Console.WriteLine("No moves found.");
                        await Application.Current.MainPage.DisplayAlert("Error", "No available moves to select.", "OK");
                        return;
                    }

                    foreach (var move in allMoves)
                    {
                        move.IsSelected = MoveIds.Contains(move.Id);
                        Moves.Add(move);
                    }

                    OnPropertyChanged(nameof(Date));
                    OnPropertyChanged(nameof(SelectedDateDisplay));
                    OnPropertyChanged(nameof(TrainingTime));
                    OnPropertyChanged(nameof(RoundsRolled));
                    OnPropertyChanged(nameof(Submissions));
                    OnPropertyChanged(nameof(Taps));
                    OnPropertyChanged(nameof(Notes));
                    OnPropertyChanged(nameof(SelfAssessment));
                    OnPropertyChanged(nameof(Moves));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading log details: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load log details: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task<bool> UpdateLogAsync()
        {
            IsBusy = true;

            try
            {
                var updatedMoves = Moves.Where(m => m.IsSelected).Select(m => m.Id).ToList();
                var updatedLog = new UpdateTrainingLogDto
                {
                    Id = _logId,
                    Date = Date,
                    TrainingTime = TrainingTime,
                    RoundsRolled = RoundsRolled,
                    Submissions = Submissions,
                    Taps = Taps,
                    Notes = Notes ?? string.Empty,
                    SelfAssessment = SelfAssessment ?? string.Empty,
                    MoveIds = updatedMoves,
                    ApplicationUserId = Preferences.Get("UserId", string.Empty)
                };

                await _trainingService.UpdateTrainingLogAsync(_logId, updatedLog, IsCoachLog);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating log: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }

            return false;
        }
    }
}

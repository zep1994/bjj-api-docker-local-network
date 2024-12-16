using System.Collections.ObjectModel;
using BjjTrainer.Models.DTO;
using BjjTrainer.Models.Move;
using BjjTrainer.Services.Moves;
using BjjTrainer.Services.Trainings;
using MvvmHelpers;

namespace BjjTrainer.ViewModels.TrainingLogs
{
    public class UpdateTrainingLogViewModel : BaseViewModel
    {
        private readonly TrainingService _trainingService;
        private readonly MoveService _moveService;
        private readonly int _logId;

        public ObservableCollection<Move> Moves { get; private set; } = new ObservableCollection<Move>();

        public DateTime Date { get; set; }
        public double TrainingTime { get; set; }
        public int RoundsRolled { get; set; }
        public int Submissions { get; set; }
        public int Taps { get; set; }
        public string Notes { get; set; }

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
                    // Populate fields from database
                    Date = log.Date;
                    SelectedDateDisplay = log.Date.ToString("yyyy-MM-dd");
                    TrainingTime = log.TrainingTime;
                    RoundsRolled = log.RoundsRolled;
                    Submissions = log.Submissions;
                    Taps = log.Taps;
                    Notes = log.Notes;

                    // Fetch and populate moves
                    var allMoves = await _moveService.GetAllMovesAsync();
                    Moves.Clear();

                    foreach (var move in allMoves)
                    {
                        move.IsSelected = log.MoveIds.Contains(move.Id);
                        Moves.Add(move);
                    }

                    // Notify UI to update bindings
                    OnPropertyChanged(nameof(Date));
                    OnPropertyChanged(nameof(SelectedDateDisplay));
                    OnPropertyChanged(nameof(TrainingTime));
                    OnPropertyChanged(nameof(RoundsRolled));
                    OnPropertyChanged(nameof(Submissions));
                    OnPropertyChanged(nameof(Taps));
                    OnPropertyChanged(nameof(Notes));
                    OnPropertyChanged(nameof(Moves));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading log details: {ex.Message}");
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
                var updatedLog = new TrainingLogDto
                {
                    Id = _logId,
                    Date = Date,
                    TrainingTime = TrainingTime,
                    RoundsRolled = RoundsRolled,
                    Submissions = Submissions,
                    Taps = Taps,
                    Notes = Notes,
                    MoveIds = updatedMoves,
                    ApplicationUserId = Preferences.Get("UserId", string.Empty)
                };

                await _trainingService.UpdateTrainingLogAsync(_logId, updatedLog);
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

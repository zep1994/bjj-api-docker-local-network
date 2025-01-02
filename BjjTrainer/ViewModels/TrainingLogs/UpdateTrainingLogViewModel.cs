using BjjTrainer.Models.DTO.TrainingLog;
using BjjTrainer.Models.Moves.BjjTrainer.Models.DTO.Moves;
using BjjTrainer.Services.Trainings;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace BjjTrainer.ViewModels.TrainingLogs
{
    public partial class UpdateTrainingLogViewModel : BaseViewModel
    {
        private readonly TrainingService _trainingService;
        private readonly int _logId;

        public ObservableCollection<UpdateMoveDto> Moves { get; set; } = new();
        public ObservableCollection<int> SelectedMoveIds { get; set; } = new();

        public DateTime Date { get; set; }
        public double TrainingTime { get; set; }
        public int RoundsRolled { get; set; }
        public int Submissions { get; set; }
        public int Taps { get; set; }
        public string? Notes { get; set; }
        public string? SelfAssessment { get; set; }
        public bool IsCoachLog { get; set; }

        public UpdateTrainingLogViewModel(int logId)
        {
            _trainingService = new TrainingService();
            _logId = logId;

            LoadLogDetails();
        }

        // ******************************** LOAD TRAINING LOG DETAILS ********************************
        private async void LoadLogDetails()
        {
            IsBusy = true;

            try
            {
                var log = await _trainingService.GetTrainingLogMoves(_logId);
                if (log != null)
                {
                    Date = log.Date;
                    TrainingTime = log.TrainingTime;
                    RoundsRolled = log.RoundsRolled;
                    Submissions = log.Submissions;
                    Taps = log.Taps;
                    Notes = log.Notes ?? string.Empty;
                    SelfAssessment = log.SelfAssessment ?? string.Empty;
                    IsCoachLog = log.IsCoachLog;

                    // Fetch full move details and update the Moves collection
                    var fullMoves = await _trainingService.GetMovesByIds(log.MoveIds);
                    Moves = new ObservableCollection<UpdateMoveDto>(fullMoves);

                    // Select the moves linked to the training log
                    SelectedMoveIds = new ObservableCollection<int>(log.MoveIds);

                    OnPropertyChanged(nameof(Date));
                    OnPropertyChanged(nameof(TrainingTime));
                    OnPropertyChanged(nameof(RoundsRolled));
                    OnPropertyChanged(nameof(Submissions));
                    OnPropertyChanged(nameof(Taps));
                    OnPropertyChanged(nameof(Notes));
                    OnPropertyChanged(nameof(SelfAssessment));
                    OnPropertyChanged(nameof(IsCoachLog));
                    OnPropertyChanged(nameof(Moves));
                    OnPropertyChanged(nameof(SelectedMoveIds));
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

        // ******************************** UPDATE SELECTED MOVES ********************************
        public void UpdateSelectedMoves(ObservableCollection<int> moveIds)
        {
            SelectedMoveIds.Clear();
            foreach (var moveId in moveIds)
            {
                SelectedMoveIds.Add(moveId);
            }
        }

        // ******************************** UPDATE TRAINING LOG ********************************
        public async Task<bool> UpdateLogAsync()
        {
            IsBusy = true;

            try
            {
                var updatedLog = new UpdateTrainingLogDto
                {
                    Date = Date,
                    TrainingTime = TrainingTime,
                    RoundsRolled = RoundsRolled,
                    Submissions = Submissions,
                    Taps = Taps,
                    Notes = Notes ?? string.Empty,
                    SelfAssessment = SelfAssessment ?? string.Empty,
                    MoveIds = SelectedMoveIds.ToList()
                };

                await _trainingService.UpdateTrainingLogAsync(_logId, updatedLog, IsCoachLog);

                await Application.Current.MainPage.Navigation.PopAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating log: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to update log: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }

            return false;
        }
    }
}

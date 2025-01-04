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

        public ObservableCollection<UpdateMoveDto> Moves { get; set; } = [];
        public ObservableCollection<int> SelectedMoveIds { get; set; } = [];

        public DateTime Date { get; set; }
        public double TrainingTime { get; set; }
        public int RoundsRolled { get; set; }
        public int Submissions { get; set; }
        public int Taps { get; set; }
        public string? Notes { get; set; }
        public string? SelfAssessment { get; set; }
        public bool IsCoachLog { get; set; }
        public int LogId { get; private set; }

        public UpdateTrainingLogViewModel(int logId)
        {
            _trainingService = new TrainingService();
            LogId = logId;

            LoadLogDetails();
        }

        // ******************************** LOAD TRAINING LOG DETAILS ********************************
        public async void LoadLogDetails()
        {
            IsBusy = true;

            try
            {
                if (LogId <= 0)
                {
                    Console.WriteLine("Invalid logId for API request.");
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid Training Log ID.", "OK");
                    return;
                }

                var log = await _trainingService.GetTrainingLogMoves(LogId);
                if (log != null)
                {
                    // Populate all fields from the log DTO
                    Date = log.Date;
                    TrainingTime = log.TrainingTime;
                    RoundsRolled = log.RoundsRolled;
                    Submissions = log.Submissions;
                    Taps = log.Taps;
                    Notes = log.Notes;
                    SelfAssessment = log.SelfAssessment;
                    IsCoachLog = log.IsCoachLog;

                    // Initialize moves even if no moveIds are returned
                    if (log.MoveIds != null && log.MoveIds.Any())
                    {
                        var fullMoves = await _trainingService.GetMovesByIds(log.MoveIds);
                        Moves = new ObservableCollection<UpdateMoveDto>(fullMoves);
                    }
                    else
                    {
                        Moves = new ObservableCollection<UpdateMoveDto>();
                        Console.WriteLine("No moves associated with this log. Initializing empty list.");
                    }

                    SelectedMoveIds = new ObservableCollection<int>(log.MoveIds ?? []);

                    // Notify UI of updated fields
                    OnPropertyChanged(nameof(Date));
                    OnPropertyChanged(nameof(TrainingTime));
                    OnPropertyChanged(nameof(RoundsRolled));
                    OnPropertyChanged(nameof(Submissions));
                    OnPropertyChanged(nameof(Taps));
                    OnPropertyChanged(nameof(Notes));
                    OnPropertyChanged(nameof(SelfAssessment));
                    OnPropertyChanged(nameof(IsCoachLog));
                    OnPropertyChanged(nameof(Moves));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Training log not found.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading log details: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load training log: {ex.Message}", "OK");
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

            // Refresh the moves list to reflect the changes
            foreach (var move in Moves)
            {
                move.IsSelected = moveIds.Contains(move.Id);
            }

            OnPropertyChanged(nameof(SelectedMoveIds));
            OnPropertyChanged(nameof(Moves));
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
                    Notes = Notes,
                    SelfAssessment = SelfAssessment,
                    MoveIds = SelectedMoveIds.ToList()
                };

                await _trainingService.UpdateTrainingLogAsync(LogId, updatedLog, IsCoachLog);

                await Shell.Current.GoToAsync($"///UpdateTrainingLogPage?logId={LogId}");
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

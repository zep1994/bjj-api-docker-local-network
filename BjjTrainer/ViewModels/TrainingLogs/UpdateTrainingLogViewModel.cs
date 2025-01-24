using BjjTrainer.Messages;
using BjjTrainer.Models.DTO.TrainingLog;
using BjjTrainer.Models.Moves.BjjTrainer.Models.DTO.Moves;
using BjjTrainer.Services.Trainings;
using BjjTrainer.Views.Components;
using CommunityToolkit.Mvvm.Messaging;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace BjjTrainer.ViewModels.TrainingLogs
{
    public partial class UpdateTrainingLogViewModel : BaseViewModel
    {
        private readonly TrainingService _trainingService;

        public ObservableCollection<UpdateMoveDto> Moves { get; set; } = new();

        public DateTime Date { get; set; }
        public double TrainingTime { get; set; }
        public int RoundsRolled { get; set; }
        public int Submissions { get; set; }
        public int Taps { get; set; }
        public string? Notes { get; set; }
        public string? SelfAssessment { get; set; }
        public bool IsCoachLog { get; set; }
        public int LogId { get; set; }

        public UpdateTrainingLogViewModel(int logId)
        {
            _trainingService = new TrainingService();
            LogId = logId;

            WeakReferenceMessenger.Default.Register<SelectedMovesUpdatedMessage>(this, (r, m) => UpdateSelectedMoves(m.Moves));

            // Load data explicitly if it hasn't been loaded yet
            if (!_isDataLoaded)
            {
                Task.Run(async () => await LoadTrainingLogDetailsAsync());
            }
        }


        private bool _isDataLoaded = false;

        public async Task LoadTrainingLogDetailsAsync()
        {
            // Prevent loading data multiple times unless explicitly requested
            if (_isDataLoaded)
                return;

            IsBusy = true;

            try
            {
                var log = await _trainingService.GetTrainingLogMoves(LogId);
                if (log != null)
                {
                    Date = log.Date;
                    TrainingTime = log.TrainingTime;
                    RoundsRolled = log.RoundsRolled;
                    Submissions = log.Submissions;
                    Taps = log.Taps;
                    Notes = log.Notes;
                    SelfAssessment = log.SelfAssessment;
                    IsCoachLog = log.IsCoachLog;

                    SyncMoves(log.Moves);
                    OnPropertyChanged(nameof(Moves));

                    _isDataLoaded = true; // Mark data as loaded
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading log details: {ex.Message}");
                await SafeDisplayAlert("Error", $"Failed to load training log: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Sync the Moves collection with the latest list from the API
        private void SyncMoves(ICollection<UpdateMoveDto> updatedMoves)
        {
            // Add or update existing moves
            foreach (var updatedMove in updatedMoves)
            {
                var existingMove = Moves.FirstOrDefault(m => m.Id == updatedMove.Id);
                if (existingMove == null)
                {
                    Moves.Add(updatedMove);
                }
                else
                {
                    existingMove.Name = updatedMove.Name; // Update name in case it changed
                    existingMove.IsSelected = true; // Ensure the move is marked as selected
                }
            }

            // Remove deselected moves
            var movesToRemove = Moves.Where(m => !updatedMoves.Any(um => um.Id == m.Id)).ToList();
            foreach (var moveToRemove in movesToRemove)
            {
                Moves.Remove(moveToRemove);
            }
        }


        public async Task EditMovesAsync()
        {
            if (Moves == null || !Moves.Any())
            {
                await SafeDisplayAlert("Notice", "No moves available. Add new moves from the selection modal.", "OK");
                return;
            }

            var modal = new MoveSelectionModal(new ObservableCollection<UpdateMoveDto>(Moves), LogId);
            await Application.Current?.MainPage?.Navigation.PushModalAsync(modal);
        }

        private async Task SafeDisplayAlert(string title, string message, string cancel)
        {
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert(title, message, cancel);
            }
            else
            {
                Console.WriteLine($"Alert: {title} - {message}");
            }
        }

        public void UpdateSelectedMoves(ObservableCollection<UpdateMoveDto> updatedMoves)
        {
            foreach (var existingMove in Moves)
            {
                existingMove.IsSelected = updatedMoves.Any(m => m.Id == existingMove.Id && m.IsSelected);
            }

            var newMoves = updatedMoves.Where(um => Moves.All(em => em.Id != um.Id)).ToList();
            foreach (var newMove in newMoves)
            {
                Moves.Add(newMove);
            }

            var removedMoves = Moves.Where(em => updatedMoves.All(um => um.Id != em.Id)).ToList();
            foreach (var removedMove in removedMoves)
            {
                Moves.Remove(removedMove);
            }

            OnPropertyChanged(nameof(Moves));
        }


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
                    Moves = new ObservableCollection<UpdateMoveDto>(Moves)
                };

                await _trainingService.UpdateTrainingLogAsync(LogId, updatedLog, IsCoachLog);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating log: {ex.Message}");
                return false;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

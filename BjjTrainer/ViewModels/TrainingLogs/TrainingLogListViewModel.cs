using System.Collections.ObjectModel;
using BjjTrainer.Models.DTO;
using BjjTrainer.Services.Trainings;
using BjjTrainer.Views.Training;
using MvvmHelpers;

namespace BjjTrainer.ViewModels.TrainingGoals
{
    public class TrainingLogListViewModel : BaseViewModel
    {
        private readonly TrainingService _trainingService;
        private readonly INavigation _navigation;

        public ObservableCollection<TrainingLogDto> TrainingLogs { get; set; } = new ObservableCollection<TrainingLogDto>();

        public TrainingLogDto SelectedLog
        {
            get => null; // Prevents retaining the selected item
            set
            {
                if (value != null)
                {
                    NavigateToUpdateLogPage(value);
                }
            }
        }

        public TrainingLogListViewModel(INavigation navigation)
        {
            _trainingService = new TrainingService();
            _navigation = navigation;
            LoadTrainingLogs();
        }

        private async void LoadTrainingLogs()
        {
            IsBusy = true;

            try
            {
                var userId = Preferences.Get("UserId", string.Empty);
                if (!string.IsNullOrEmpty(userId))
                {
                    var logs = await _trainingService.GetTrainingLogsAsync(userId);
                    TrainingLogs.Clear();

                    foreach (var log in logs.Take(10))
                    {
                        TrainingLogs.Add(log);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading training logs: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void NavigateToUpdateLogPage(TrainingLogDto selectedLog)
        {
            await _navigation.PushAsync(new UpdateTrainingLogPage(selectedLog.Id));
        }
    }
}

using BjjTrainer.Models.Training;
using BjjTrainer.Services.Training;
using BjjTrainer.Views.Training;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace BjjTrainer.ViewModels.Training
{
    public partial class TrainingSessionListViewModel : BaseViewModel
    {
        private readonly TrainingSessionService _trainingSessionService;
        private TrainingSession _selectedSession;

        public ObservableCollection<TrainingSession> TrainingSessions { get; private set; }

        public TrainingSessionListViewModel(TrainingSessionService trainingSessionService)
        {
            _trainingSessionService = trainingSessionService;
            TrainingSessions = new ObservableCollection<TrainingSession>();
            LoadTrainingSessionsCommand = new Command(async () => await LoadTrainingSessions());

            // Load sessions immediately on initialization
            LoadTrainingSessionsCommand.Execute(null);
        }

        public TrainingSession SelectedSession
        {
            get => _selectedSession;
            set
            {
                _selectedSession = value;
                OnPropertyChanged();
                if (_selectedSession != null)
                {
                    // Navigate to detail page with selected session
                    Shell.Current.GoToAsync($"{nameof(TrainingSessionDetailPage)}?SessionId={_selectedSession.Id}");
                    SelectedSession = null; // Reset after navigation
                }
            }
        }

        public Command LoadTrainingSessionsCommand { get; }

        private async Task LoadTrainingSessions()
        {
            var sessions = await _trainingSessionService.GetAllTrainingSessionsAsync();
            if (sessions != null)
            {
                TrainingSessions.Clear();
                foreach (var session in sessions)
                {
                    TrainingSessions.Add(session);
                }
            }
        }
    }
}

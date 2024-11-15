using BjjTrainer.Models.Training;
using BjjTrainer.Services.Training;
using BjjTrainer.ViewModels.Training;
using System.Collections.ObjectModel;

namespace BjjTrainer.Views.Training
{
    public partial class TrainingSessionListPage : ContentPage
    {
        private readonly TrainingSessionService _trainingSessionService;
        public ObservableCollection<TrainingSession> TrainingSessions { get; set; }

        public TrainingSessionListPage()
        {
            InitializeComponent();
            TrainingSessions = new ObservableCollection<TrainingSession>();
            BindingContext = new TrainingSessionListViewModel(new TrainingSessionService());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is TrainingSessionListViewModel vm)
            {
                vm.LoadTrainingSessionsCommand.Execute(null);
            }
        }


        private async Task LoadTrainingSessionsAsync()
        {
            try
            {
                var sessions = await _trainingSessionService.GetAllTrainingSessionsAsync();
                TrainingSessions.Clear();
                foreach (var session in sessions)
                {
                    TrainingSessions.Add(session);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load training sessions: {ex.Message}", "OK");
            }
        }
    }
}
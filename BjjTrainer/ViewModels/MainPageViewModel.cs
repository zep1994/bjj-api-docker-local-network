using BjjTrainer.Models.TrainingGoal;
using BjjTrainer.Services.Events;
using BjjTrainer.Services.Trainings;
using BjjTrainer.Services.Users;
using BjjTrainer.Views.Moves;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace BjjTrainer.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly TrainingService _trainingService;
        private readonly EventService _eventService;
        private readonly UserProgressService _userProgressService;
        private TrainingSummaryModel _trainingSummary;


        public ObservableCollection<TopMovesModel> TopMoves { get; set; } = new ObservableCollection<TopMovesModel>();
        public ObservableCollection<TrainingGoal> TrainingGoals { get; set; } = new ObservableCollection<TrainingGoal>();

        public string TotalTrainingLogs { get; private set; }
        public string TotalTrainingTime { get; private set; }
        public string AverageTrainingTime { get; private set; }

        public string TotalGoalsAchieved { get; private set; }
        public string TotalMovesPracticed { get; private set; }

        public MainPageViewModel()
        {
            _trainingService = new TrainingService();
            _eventService = new EventService();
            _userProgressService = new UserProgressService();

            LoadData();
            _ = LoadTopMovesAsync();

        }

        public TrainingSummaryModel TrainingSummary
        {
            get => _trainingSummary;
            set => SetProperty(ref _trainingSummary, value);
        }

        private async Task LoadTopMovesAsync()
        {
            try
            {
                // Replace "userId" with your actual logic to get the user's ID
                var userId = Preferences.Get("UserId", string.Empty);
                if (string.IsNullOrEmpty(userId))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "User not logged in.", "OK");
                    return;
                }

                var topMoves = await _trainingService.GetTopMovesAsync();

                TopMoves.Clear();
                foreach (var move in topMoves)
                {
                    TopMoves.Add(move);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load top moves: {ex.Message}", "OK");
            }
        }


        private async void LoadData()
        {
            var moves = await _trainingService.GetTopMovesAsync();
            foreach (var move in moves)
                TopMoves.Add(move);

            TrainingSummary = await _trainingService.GetTrainingSummaryAsync();


            //var goals = await _eventService.GetUserGoalsAsync();
            //foreach (var goal in goals)
            //    TrainingGoals.Add(goal);

            //var progress = await _userProgressService.GetUserProgressAsync();
            //TotalGoalsAchieved = progress.GoalsAchieved.ToString();
            //TotalMovesPracticed = progress.MovesPracticed.ToString();
        }
    }
}

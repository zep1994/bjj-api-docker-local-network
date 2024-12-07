using BjjTrainer.Models.DTO;
using BjjTrainer.Models.Move;
using BjjTrainer.Models.TrainingGoal;
using BjjTrainer.Services.Moves;
using BjjTrainer.Services.TrainingGoals;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace BjjTrainer.ViewModels.Trainings
{
    public class TrainingGoalViewModel : BaseViewModel
    {
        private readonly TrainingGoalService _trainingGoalService;
        private readonly MoveService _moveService;

        public ObservableCollection<Move> Moves { get; set; } = new ObservableCollection<Move>();
        public ObservableCollection<TrainingGoal> TrainingGoals { get; set; } = new ObservableCollection<TrainingGoal>();

        public string Notes { get; set; } = string.Empty;
        public DateTime GoalDate { get; set; } = DateTime.Now;

        public Command SubmitGoalCommand { get; }
        public Command LoadGoalsCommand { get; }

        public TrainingGoalViewModel()
        {
            _trainingGoalService = new TrainingGoalService();
            _moveService = new MoveService();

            SubmitGoalCommand = new Command(async () => await SubmitGoalAsync());
            LoadGoalsCommand = new Command(async () => await LoadGoalsAsync());

            LoadMovesAsync();
        }

        private async Task LoadMovesAsync()
        {
            var moves = await _moveService.GetAllMovesAsync();
            Moves.Clear();
            foreach (var move in moves)
            {
                Moves.Add(move);
            }
        }

        private async Task SubmitGoalAsync()
        {
            var selectedMoveIds = Moves.Where(m => m.IsSelected).Select(m => m.Id).ToList();
            var dto = new CreateTrainingGoalDto
            {
                ApplicationUserId = Preferences.Get("UserId", string.Empty),
                GoalDate = GoalDate,
                Notes = Notes,
                MoveIds = selectedMoveIds
            };

            var success = await _trainingGoalService.CreateTrainingGoalAsync(dto);
            if (success)
            {
                await LoadGoalsAsync();
            }
        }

        private async Task LoadGoalsAsync()
        {
            var userId = Preferences.Get("UserId", string.Empty);
            var goals = await _trainingGoalService.GetTrainingGoalsAsync(userId);

            TrainingGoals.Clear();
            foreach (var goal in goals)
            {
                TrainingGoals.Add(goal);
            }
        }
    }
}
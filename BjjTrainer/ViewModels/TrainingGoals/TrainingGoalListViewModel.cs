using BjjTrainer.Models.TrainingGoal;
using BjjTrainer.Services.TrainingGoals;
using BjjTrainer.Views.TrainingGoals;
using MvvmHelpers;
using System.Collections.ObjectModel;
using Command = Microsoft.Maui.Controls.Command; // Specify the Command namespace to resolve ambiguity

namespace BjjTrainer.ViewModels.TrainingGoals;

public class TrainingGoalListViewModel : BaseViewModel
{
    private readonly TrainingGoalService _trainingGoalService;

    public ObservableCollection<TrainingGoal> TrainingGoals { get; set; } = new();

    public Command AddNewGoalCommand { get; }

    public TrainingGoalListViewModel()
    {
        _trainingGoalService = new TrainingGoalService();

        LoadGoalsAsync();
    }

    public async Task LoadGoalsAsync()
    {
        IsBusy = true;

        try
        {
            var userId = Preferences.Get("UserId", string.Empty);
            var goals = await _trainingGoalService.GetTrainingGoalsAsync(userId);

            TrainingGoals.Clear();
            foreach (var goal in goals)
            {
                TrainingGoals.Add(goal);
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions if needed
        }
        finally
        {
            IsBusy = false;
        }
    }
}

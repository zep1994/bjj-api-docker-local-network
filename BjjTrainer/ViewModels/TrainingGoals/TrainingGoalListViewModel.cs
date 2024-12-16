using BjjTrainer.Models.TrainingGoal;
using BjjTrainer.Services.TrainingGoals;
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

        Task.Run(async () => await LoadGoalsAsync());
    }

    public async Task DeleteGoalAsync(int goalId)
    {
        IsBusy = true;

        try
        {
            var success = await _trainingGoalService.DeleteTrainingGoalAsync(goalId);
            if (success)
            {
                var goalToRemove = TrainingGoals.FirstOrDefault(g => g.Id == goalId);
                if (goalToRemove != null)
                {
                    TrainingGoals.Remove(goalToRemove);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting goal: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    public async Task LoadGoalsAsync()
    {
        IsBusy = true;

        try
        {
            var goals = await _trainingGoalService.GetTrainingGoalsAsync();
            Console.WriteLine($"Loaded {goals.Count} training goals.");

            TrainingGoals.Clear();
            foreach (var goal in goals)
            {
                TrainingGoals.Add(goal);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading training goals: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

}

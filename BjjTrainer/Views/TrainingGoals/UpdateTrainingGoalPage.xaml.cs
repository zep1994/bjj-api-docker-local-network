using BjjTrainer.ViewModels.TrainingGoals;

namespace BjjTrainer.Views.TrainingGoals;

[QueryProperty(nameof(GoalId), "goalId")]
public partial class UpdateTrainingGoalPage : ContentPage
{
    public int GoalId { get; set; }
    private readonly UpdateTrainingGoalViewModel _viewModel;

    public UpdateTrainingGoalPage()
    {
        InitializeComponent();
        _viewModel = new UpdateTrainingGoalViewModel(GoalId);
        BindingContext = _viewModel;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        // Load goal details when the page appears
        if (GoalId > 0)
        {
            await _viewModel.LoadGoalDetailsAsync();
        }
    }

    private async void OnSaveGoalClicked(object sender, EventArgs e)
    {
        var success = await _viewModel.SaveGoalAsync();
        if (success)
        {
            await DisplayAlert("Success", "Training goal updated successfully.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await DisplayAlert("Error", "Failed to update training goal.", "OK");
        }
    }
}

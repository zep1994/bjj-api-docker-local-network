using BjjTrainer.ViewModels.TrainingGoals;

namespace BjjTrainer.Views.TrainingGoals;

public partial class ShowTrainingGoalPage : ContentPage
{
    private readonly ShowTrainingGoalViewModel _viewModel;

    public ShowTrainingGoalPage(int goalId)
    {
        InitializeComponent();
        _viewModel = new ShowTrainingGoalViewModel(goalId);
        BindingContext = _viewModel;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnUpdateButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UpdateTrainingGoalPage(_viewModel.GoalId));
    }
}
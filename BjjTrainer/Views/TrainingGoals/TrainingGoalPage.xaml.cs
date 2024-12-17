using BjjTrainer.ViewModels.TrainingGoals;

namespace BjjTrainer.Views.TrainingGoals;

public partial class TrainingGoalPage : ContentPage
{
    private readonly TrainingGoalViewModel _viewModel;

    public TrainingGoalPage()
    {
        InitializeComponent();
        _viewModel = new TrainingGoalViewModel();
        BindingContext = _viewModel;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var success = await _viewModel.SaveGoalAsync();
        if (success)
        {
            await DisplayAlert("Success", "Training goal saved successfully.", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Failed to save training goal.", "OK");
        }
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}

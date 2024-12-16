using BjjTrainer.ViewModels.TrainingGoals;
using Syncfusion.Maui.Core.Carousel;

namespace BjjTrainer.Views.TrainingGoals;

public partial class TrainingGoalListPage : ContentPage
{
    private readonly TrainingGoalListViewModel _viewModel;

    public TrainingGoalListPage()
    {
        InitializeComponent();
        _viewModel = new TrainingGoalListViewModel();
        BindingContext = _viewModel;

        // Load goals asynchronously after the page initializes
        Task.Run(async () => await _viewModel.LoadGoalsAsync());
    }

    private async void OnDeleteGoalClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int goalId)
        {
            var confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this goal?", "Yes", "No");
            if (confirm)
            {
                await ((TrainingGoalListViewModel)BindingContext).DeleteGoalAsync(goalId);
            }
        }
    }

    private async void OnViewGoalClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int goalId)
        {
            await Shell.Current.GoToAsync($"///UpdateTrainingGoalPage?goalId={goalId}");
        }
    }

    private async void OnNewGoalClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TrainingGoalPage");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Reload goals when the page appears
        Task.Run(async () => await _viewModel.LoadGoalsAsync());
    }
}

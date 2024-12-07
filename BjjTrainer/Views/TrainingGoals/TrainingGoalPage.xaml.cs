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

        // Explicitly set bindings
        GoalDatePicker.Date = _viewModel.GoalDate;
        NotesEditor.Text = _viewModel.Notes;
        MovesCollectionView.ItemsSource = _viewModel.Moves;
    }

    private async void OnSaveGoalClicked(object sender, EventArgs e)
    {
        // Update the ViewModel with UI inputs
        _viewModel.GoalDate = GoalDatePicker.Date;
        _viewModel.Notes = NotesEditor.Text;

        var success = await _viewModel.SaveGoalAsync();
        if (success)
        {
            await DisplayAlert("Success", "Training goal saved successfully.", "OK");
            await Shell.Current.GoToAsync(".."); // Navigate back to the TrainingGoalListPage
        }
        else
        {
            await DisplayAlert("Error", "Failed to save training goal. Please check the data and try again.", "OK");
        }
    }

}

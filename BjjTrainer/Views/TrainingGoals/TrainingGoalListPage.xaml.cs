using BjjTrainer.ViewModels.TrainingGoals;

namespace BjjTrainer.Views.TrainingGoals;

public partial class TrainingGoalListPage : ContentPage
{
    public TrainingGoalListPage()
    {
        InitializeComponent();
        BindingContext = new TrainingGoalListViewModel();
    }

    private async void OnNewGoalClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TrainingGoalPage");
    }
}

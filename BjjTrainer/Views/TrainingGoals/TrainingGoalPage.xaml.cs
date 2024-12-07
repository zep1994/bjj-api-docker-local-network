using BjjTrainer.ViewModels.Trainings;

namespace BjjTrainer.Views.TrainingGoals;

public partial class TrainingGoalPage : ContentPage
{
    public TrainingGoalPage()
    {
        InitializeComponent();
        BindingContext = new TrainingGoalViewModel();

    }
}
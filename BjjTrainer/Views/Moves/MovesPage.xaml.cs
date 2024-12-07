using BjjTrainer.ViewModels.Moves;

namespace BjjTrainer.Views.Moves;

public partial class MovesPage : ContentPage
{
    public MovesPage()
    {
        InitializeComponent();
        BindingContext = new MovesViewModel(); // Set BindingContext here
    }
protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MovesViewModel viewModel)
        {
            viewModel.LoadMovesCommand.Execute(null);
        }
    }
}
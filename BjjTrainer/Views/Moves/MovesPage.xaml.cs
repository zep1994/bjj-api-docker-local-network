using BjjTrainer.ViewModels.Moves;

namespace BjjTrainer.Views.Moves;

public partial class MovesPage : ContentPage
{
    public MovesPage()
    {
        InitializeComponent();
        BindingContext = new MovesViewModel(); // Set BindingContext here
    }
}
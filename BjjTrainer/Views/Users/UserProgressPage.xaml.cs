using BjjTrainer.ViewModels.Users;

namespace BjjTrainer.Views.Users;

public partial class UserProgressPage : ContentPage
{
    private readonly UserProgressViewModel _viewModel;

    public UserProgressPage()
    {
        InitializeComponent();
        _viewModel = new UserProgressViewModel();
        BindingContext = _viewModel;
    }

    private void OnPageLoaded(object sender, EventArgs e)
    {
        _viewModel.LoadProgressCommand.Execute(null);
    }
}
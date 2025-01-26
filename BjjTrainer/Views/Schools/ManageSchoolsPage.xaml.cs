using BjjTrainer.ViewModels.Schools;

namespace BjjTrainer.Views.Schools;

public partial class ManageSchoolsPage : ContentPage
{
    public ManageSchoolsPage(ManageSchoolsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
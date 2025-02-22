using BjjTrainer.ViewModels.Students;
using BjjTrainer.Views.Coaches;

namespace BjjTrainer.Views.Students
{
    public partial class ManageStudentsPage : ContentPage
    {
        public ManageStudentsPage(ManageStudentsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"///{nameof(CoachManagementPage)}");
        }
    }
}
using BjjTrainer.ViewModels.Coaches;
using BjjTrainer.Views.Schools;

namespace BjjTrainer.Views.Coaches
{
    public partial class CoachManagementPage : ContentPage
    {
        private readonly CoachManagementViewModel _viewModel;

        public CoachManagementPage(CoachManagementViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }

        private async void OnEditSchoolClicked(object sender, EventArgs e)
        {
            if (_viewModel.CoachSchool != null)
            {
                await Shell.Current.GoToAsync($"///UpdateSchoolPage?id={_viewModel.CoachSchool.Id}");
            }
        }
    }
}
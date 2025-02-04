using BjjTrainer.Models.Schools;
using BjjTrainer.ViewModels.Schools;

namespace BjjTrainer.Views.Schools
{
    public partial class ManageSchoolsPage : ContentPage
    {
        private readonly ManageSchoolsViewModel _viewModel;

        public ManageSchoolsPage(ManageSchoolsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }

        private async void OnCreateSchoolClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CreateSchoolPage));
        }

        private async void OnEditSchoolClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is School school)
            {
                await Shell.Current.GoToAsync($"{nameof(UpdateSchoolPage)}?id={school.Id}");
            }
        }

        private async void OnDeleteSchoolClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is School school)
            {
                await _viewModel.DeleteSchoolAsync(school);
            }
        }
    }
}

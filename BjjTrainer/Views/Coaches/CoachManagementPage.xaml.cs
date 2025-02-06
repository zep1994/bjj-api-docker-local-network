using BjjTrainer.ViewModels.Coaches;
using Newtonsoft.Json;

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
                string schoolJson = JsonConvert.SerializeObject(_viewModel.CoachSchool);
                await Shell.Current.GoToAsync($"///UpdateSchoolPage?schoolJson={Uri.EscapeDataString(schoolJson)}");
            }
            else
            {
                Console.WriteLine("Error: No school data available.");
            }
        }

        private async void OnViewLessonsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///LessonsPage");
        }

        private async void OnManageStudentsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///ManageStudentsPage");
        }

        private async void OnViewEventsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///CalendarPage");
        }
    }
}

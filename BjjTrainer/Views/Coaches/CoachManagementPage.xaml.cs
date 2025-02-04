using BjjTrainer.ViewModels.Coaches;
using BjjTrainer.Views.Events;
using BjjTrainer.Views.Lessons;
using BjjTrainer.Views.Moves;
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
                await Shell.Current.GoToAsync($"{nameof(UpdateSchoolPage)}?id={_viewModel.CoachSchool.Id}");
            }
        }

        private async void OnViewLessonsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(LessonsPage));
        }

        private async void OnViewMovesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(MovesPage));
        }

        private async void OnViewEventsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CalendarPage));
        }
    }
}
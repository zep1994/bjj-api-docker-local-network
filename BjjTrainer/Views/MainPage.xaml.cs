namespace BjjTrainer.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLessonsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LessonsPage");
        }

        private async void OnTrainingLogClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//TrainingLogFormPage");
        }

        private async void OnGoalsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//TrainingGoalListPage");
        }

        private async void OnUserProgressClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//UserProgressPage");
        }

        private async void OnCalendarClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//CalendarPage");
        }

        private async void OnMovesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MovesPage");
        }

        private async void OnAccountClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//UserProfilePage");
        }
    }
}
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

        private async void OnUserLessonsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//FavoritesPage");
        }

        private async void OnUserProgressClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//UserProgressPage");
        }

        private async void OnCalendarClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//CalendarPage");
        }

        private async void OnAchievementsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AchievementsPage");
        }

        private async void OnTrainingGoalsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//TrainingGoalsPage");
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
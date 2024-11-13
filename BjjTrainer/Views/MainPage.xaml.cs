using BjjTrainer.Views.Lessons;

namespace BjjTrainer.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnViewLessonsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LessonsPage");
        }
    }
}

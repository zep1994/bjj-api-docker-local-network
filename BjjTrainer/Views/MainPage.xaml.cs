using Microsoft.Extensions.DependencyInjection;
using BjjTrainer.ViewModels;
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
            await Navigation.PushAsync(new LessonsPage());
        }
    }
}

using BjjTrainer.ViewModels;
using BjjTrainer.Views;
using BjjTrainer.Views.Lessons;
using BjjTrainer.Views.Users;

namespace BjjTrainer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            this.Navigated += OnNavigated;
            BindingContext = new MainPageViewModel();
        }

        private void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
            // Hide the Flyout after navigation
            this.FlyoutIsPresented = false;
        }
    }
}

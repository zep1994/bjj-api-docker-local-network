using BjjTrainer.ViewModels;

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

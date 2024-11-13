using BjjTrainer.Views.Users;

namespace BjjTrainer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Check if token is stored and navigate accordingly
            if (Preferences.ContainsKey("AuthToken"))
            {
                MainPage = new AppShell(); // Main app page
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage()); // Navigate to login
            }
        }
    }
}

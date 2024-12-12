using BjjTrainer.Views.Users;
using System.Diagnostics;

namespace BjjTrainer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Debug.WriteLine($"Unhandled Exception: {e.ExceptionObject}");
            };

            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Debug.WriteLine($"Unobserved Task Exception: {e.Exception.Message}");
                e.SetObserved();
            };

            MainPage = new AppShell();
        }
    }
}

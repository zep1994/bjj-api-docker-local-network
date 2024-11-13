using System.Windows.Input;

namespace BjjTrainer.ViewModels
{
    public partial class MainPageViewModel : BindableObject
    {
        public ICommand NavigateToLessonsCommand { get; }
        public ICommand LogoutCommand { get; }


        public MainPageViewModel()
        {
            NavigateToLessonsCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("LessonsPage"); // Update the route according to your Shell configuration
            });

            LogoutCommand = new Command(ExecuteLogout);
        }

        private async void ExecuteLogout()
        {
            // Clear the authentication token
            Preferences.Remove("AuthToken");

            // Navigate to LoginPage
            await Shell.Current.GoToAsync("///LoginPage"); 
        }
    }
}

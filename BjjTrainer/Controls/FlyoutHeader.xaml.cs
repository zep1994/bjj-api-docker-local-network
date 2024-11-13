namespace BjjTrainer.Controls
{
    public partial class FlyoutHeader : ContentView
    {
        public FlyoutHeader()
        {
            InitializeComponent();
        }

        // Navigate to the MainPage
        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        // Navigate to the LoginPage after clearing the AuthToken
        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Preferences.Remove("AuthToken"); // Clear the token for logout
            await Shell.Current.GoToAsync("///LoginPage");
        }
    }
}

using BjjTrainer.Services.Users;

namespace BjjTrainer.Views.Users;

public partial class LogoutPage : ContentPage
{
    private readonly UserService _userService;

    public LogoutPage()
    {
        InitializeComponent();
        _userService = new UserService();
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool success = await _userService.LogoutAsync();
        if (success)
        {
            await DisplayAlert("Success", "Logged out", "OK");
            // Return to login page if needed
        }
    }
}
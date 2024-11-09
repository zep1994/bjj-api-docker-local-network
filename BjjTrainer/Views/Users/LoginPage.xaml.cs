using BjjTrainer.Services.Users;

namespace BjjTrainer.Views.Users;

public partial class LoginPage : ContentPage
{
    private readonly UserService _userService;

    public LoginPage()
    {
        InitializeComponent();
        _userService = new UserService();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string token = await _userService.LoginAsync(UsernameEntry.Text, PasswordEntry.Text);
        if (!string.IsNullOrEmpty(token))
        {
            await DisplayAlert("Success", "Login Successful", "OK");
            // Navigate to a main app page if desired
        }
        else
        {
            await DisplayAlert("Error", "Login Failed", "OK");
        }
    }
}
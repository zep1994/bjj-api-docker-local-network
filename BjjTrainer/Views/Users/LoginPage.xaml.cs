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

    private async void OnSignupLabelTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignupPage());
    }

    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        await DisplayAlert("Forgot Password", "Password recovery process here.", "OK");
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string token = await _userService.LoginAsync(UsernameEntry.Text, PasswordEntry.Text);
        if (!string.IsNullOrEmpty(token))
        {
            await DisplayAlert("Success", "Login Successful", "OK");
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Error", "Login Failed", "OK");
        }
    }
}
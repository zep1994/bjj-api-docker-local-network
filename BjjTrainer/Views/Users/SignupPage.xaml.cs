using BjjTrainer.Services.Users;

namespace BjjTrainer.Views.Users;

public partial class SignupPage : ContentPage
{
    private readonly UserService _userService;

    public SignupPage()
    {
        InitializeComponent();
        _userService = new UserService();
    }

    private async void OnSignupClicked(object sender, EventArgs e)
    {
        bool success = await _userService.SignupAsync(UsernameEntry.Text, EmailEntry.Text, PasswordEntry.Text);
        if (success)
        {
            await DisplayAlert("Success", "Signup Successful", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Signup Failed", "OK");
        }
    }
}
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


    private async void OnLoginLabelTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }


    private async void OnSignupClicked(object sender, EventArgs e)
    {
        try
        {
            string token = await _userService.SignupAsync(UsernameEntry.Text, EmailEntry.Text, PasswordEntry.Text);
            await DisplayAlert("Success", "Signup Successful", "OK");
            Application.Current.MainPage = new AppShell();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Signup Failed", ex.Message, "OK");
        }
    }

}
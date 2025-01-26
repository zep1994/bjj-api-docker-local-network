using BjjTrainer.Models.Schools;
using BjjTrainer.Services.Users;
using Microsoft.Maui.Storage;

namespace BjjTrainer.Views.Users;

public partial class SignupPage : ContentPage
{
    private readonly UserService _userService;

    public SignupPage()
    {
        InitializeComponent();
        _userService = new UserService();

        // Load available schools when the page loads
        LoadSchools();
    }

    private async void LoadSchools()
    {
        try
        {
            var schools = await _userService.GetAllSchoolsAsync();
            SchoolPicker.ItemsSource = schools;
            SchoolPicker.ItemDisplayBinding = new Binding("Name");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load schools: {ex.Message}", "OK");
        }
    }

    private async void OnSignupClicked(object sender, EventArgs e)
    {
        try
        {
            if (SchoolPicker.SelectedItem is not School selectedSchool)
            {
                await DisplayAlert("Error", "Please select a school.", "OK");
                return;
            }

            var role = RolePicker.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(role))
            {
                await DisplayAlert("Error", "Please select a role.", "OK");
                return;
            }

            var isCoach = role == "Coach";

            string token = await _userService.SignupAsync(
                UsernameEntry.Text,
                EmailEntry.Text,
                PasswordEntry.Text,
                selectedSchool.Id,
                isCoach
            );

            await DisplayAlert("Success", "Signup Successful", "OK");
            Application.Current.MainPage = new AppShell();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Signup Failed", ex.Message, "OK");
        }
    }

    private async void OnLoginLabelTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}

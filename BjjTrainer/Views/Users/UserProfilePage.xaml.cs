using BjjTrainer.ViewModels.Users;

namespace BjjTrainer.Views.Users;

public partial class UserProfilePage : ContentPage
{
	public UserProfilePage()
	{
		InitializeComponent();
        BindingContext = new UserProfileViewModel();

    }

    private async void OnEditProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        var confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
        if (confirm)
        {
            // Handle logout logic
            await Navigation.PopToRootAsync();
        }
    }
}
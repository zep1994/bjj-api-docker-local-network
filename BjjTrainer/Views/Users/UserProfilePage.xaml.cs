using BjjTrainer.ViewModels.Users;

namespace BjjTrainer.Views.Users;

public partial class UserProfilePage : ContentPage
{
	public UserProfilePage()
	{
		InitializeComponent();
        BindingContext = new UserProfileViewModel();

    }
}
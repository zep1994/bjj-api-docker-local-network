using BjjTrainer.Services.Users;
using MvvmHelpers;

namespace BjjTrainer.ViewModels.Users
{
    public partial class UserProfileViewModel : BaseViewModel
    {
        private readonly UserService _userService;

        public string Username { get; set; }
        public string Email { get; set; }

        public UserProfileViewModel()
        {
            _userService = new UserService();
            LoadUserDataAsync();
        }

        private async Task LoadUserDataAsync()
        {
            var token = Preferences.Get("AuthToken", string.Empty);
            if (!string.IsNullOrEmpty(token))
            {
                var userId = UserService.GetUserIdFromToken(token);
                var user = await _userService.GetUserByIdAsync(userId);

                if (user != null)
                {
                    Username = user.Username;
                    Email = user.Email;
                    OnPropertyChanged(nameof(Username));
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
    }
}
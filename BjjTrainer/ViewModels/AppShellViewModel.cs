using MvvmHelpers;

namespace BjjTrainer.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        private string _profileImage;
        public string ProfileImage
        {
            get => _profileImage;
            set => SetProperty(ref _profileImage, value);
        }

        private string _pageTitle;
        public string PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
        }

        private bool _isProfileVisible;
        public bool IsProfileVisible
        {
            get => _isProfileVisible;
            set => SetProperty(ref _isProfileVisible, value);
        }

        public AppShellViewModel()
        {
            ProfileImage = "default_profile.png";
            Title = "Roll Call";
        }
    }
}

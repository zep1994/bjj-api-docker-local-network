using BjjTrainer.ViewModels;
using BjjTrainer.Views;
using BjjTrainer.Views.Lessons;
using BjjTrainer.Views.Users;

namespace BjjTrainer
{
    public partial class AppShell : Shell
    {
        private MenuItem logoutMenuItem;

        public AppShell()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }
}

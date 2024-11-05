using System.Windows.Input;

namespace BjjTrainer.ViewModels
{
    public partial class MainPageViewModel : BindableObject
    {
        public ICommand NavigateToLessonsCommand { get; }

        public MainPageViewModel()
        {
            NavigateToLessonsCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("//LessonsPage"); // Update the route according to your Shell configuration
            });
        }
    }
}

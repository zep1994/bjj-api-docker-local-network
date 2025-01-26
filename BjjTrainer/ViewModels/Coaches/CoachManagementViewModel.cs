using BjjTrainer.Views.Schools;
using MvvmHelpers;
using System.Windows.Input;

namespace BjjTrainer.ViewModels.Coaches
{
    public class CoachManagementViewModel : BaseViewModel
    {
        public ICommand NavigateToManageSchoolsCommand { get; }
        public ICommand NavigateToManageLessonsCommand { get; }
        public ICommand NavigateToManageMovesCommand { get; }
        public ICommand NavigateToManageEventsCommand { get; }

        public CoachManagementViewModel()
        {
            NavigateToManageSchoolsCommand = new Command(async () =>
                await Shell.Current.GoToAsync(nameof(ManageSchoolsPage)));

            //NavigateToManageLessonsCommand = new Command(async () =>
            //    await Shell.Current.GoToAsync(nameof(ManageLessonsPage)));

            //NavigateToManageMovesCommand = new Command(async () =>
            //    await Shell.Current.GoToAsync(nameof(ManageMovesPage)));

            //NavigateToManageEventsCommand = new Command(async () =>
            //    await Shell.Current.GoToAsync(nameof(ManageEventsPage)));
        }
    }
}

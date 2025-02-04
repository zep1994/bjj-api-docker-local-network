using BjjTrainer.Models.Schools;
using BjjTrainer.Services.Schools;
using BjjTrainer.Views.Schools;
using MvvmHelpers;
using System.Windows.Input;

namespace BjjTrainer.ViewModels.Coaches
{
    public class CoachManagementViewModel : BaseViewModel
    {
        private readonly SchoolService _schoolService;
        private School _coachSchool;

        public School CoachSchool
        {
            get => _coachSchool;
            set
            {
                _coachSchool = value;
                OnPropertyChanged(); // Notify UI
                OnPropertyChanged(nameof(HasSchool));
            }
        }
        public bool HasSchool => CoachSchool != null;

        public CoachManagementViewModel(SchoolService schoolService)
        {
            _schoolService = schoolService;
            LoadCoachSchool();
        }

        private async void LoadCoachSchool()
        {
            try
            {
                var userId = Preferences.Get("UserId", string.Empty);
                if (string.IsNullOrEmpty(userId)) return;

                CoachSchool = await _schoolService.GetSchoolByIdAsync(userId);
                OnPropertyChanged(nameof(CoachSchool));
                OnPropertyChanged(nameof(HasSchool));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading coach's school: {ex.Message}");
            }
        }
    }
}
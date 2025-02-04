using System.Windows.Input;
using BjjTrainer.Models.Schools;
using BjjTrainer.Services.Schools;
using MvvmHelpers;

namespace BjjTrainer.ViewModels.Schools
{
    public class UpdateSchoolViewModel : BaseViewModel
    {
        private readonly SchoolService _schoolService;

        public School School { get; set; } = new School();

        public ICommand SaveCommand { get; }

        public UpdateSchoolViewModel(SchoolService schoolService)
        {
            _schoolService = schoolService;
            SaveCommand = new Command(async () => await SaveSchool());
        }

        public async Task LoadSchoolAsync(int schoolId)
        {
            try
            {
               // School = await _schoolService.GetSchoolByIdAsync(schoolId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading school: {ex.Message}");
            }
        }

        private async Task SaveSchool()
        {
            try
            {
                await _schoolService.UpdateSchoolAsync(School.Id, School);
                await Shell.Current.GoToAsync(".."); // Navigate back
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating school: {ex.Message}");
            }
        }
    }
}

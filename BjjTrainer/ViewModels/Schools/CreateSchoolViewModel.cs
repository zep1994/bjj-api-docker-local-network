using System.Windows.Input;
using BjjTrainer.Models.Schools;
using BjjTrainer.Services.Schools;
using MvvmHelpers;

namespace BjjTrainer.ViewModels.Schools
{
    public class CreateSchoolViewModel : BaseViewModel
    {
        private readonly SchoolService _schoolService;

        public School School { get; set; } = new School();

        public ICommand SaveCommand { get; }

        public CreateSchoolViewModel(SchoolService schoolService)
        {
            _schoolService = schoolService;
            SaveCommand = new Command(async () => await SaveSchool());
        }

        private async Task SaveSchool()
        {
            try
            {
                await _schoolService.CreateSchoolAsync(School);
                await Shell.Current.GoToAsync(".."); // Navigate back
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving school: {ex.Message}");
            }
        }
    }
}

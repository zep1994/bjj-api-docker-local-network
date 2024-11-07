using BjjTrainer.Models.DTO;
using BjjTrainer.Services.Lessons;
using MvvmHelpers;

namespace BjjTrainer.ViewModels
{
    public partial class SubLessonDetailsViewModel : BaseViewModel
    {
        private readonly SubLessonService _subLessonService;

        private SubLessonDetailsDto _subLessonDetails;
        public SubLessonDetailsDto SubLessonDetails
        {
            get => _subLessonDetails;
            set
            {
                _subLessonDetails = value;
                OnPropertyChanged(nameof(SubLessonDetails)); // Trigger UI update
            }
        }

        public SubLessonDetailsViewModel(int subLessonId)
        {
            _subLessonService = new SubLessonService();
            SubLessonDetails = new SubLessonDetailsDto(); // Default value to avoid null bindings
            _ = LoadSubLessonDetailsAsync(subLessonId);
        }

        // File: SubLessonDetailsViewModel.cs
        public async Task LoadSubLessonDetailsAsync(int subLessonId)
        {
            try
            {
                SubLessonDetails = await _subLessonService.GetSubLessonDetailsByIdAsync(subLessonId);
                OnPropertyChanged(nameof(SubLessonDetails));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load sublesson details.", "OK");
            }
        }
    }
}

using BjjTrainer.Models;
using BjjTrainer.Services.Lessons;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace BjjTrainer.ViewModels.Lessons
{
    public class SubLessonViewModel : BaseViewModel
    {
        private readonly SubLessonService _subLessonService;
        private ObservableCollection<SubLesson> _subLessons;

        public ObservableCollection<SubLesson> SubLessons
        {
            get => _subLessons;
            set => SetProperty(ref _subLessons, value); // Ensure this is an ObservableCollection
        }

        public string SectionTitle { get; set; }

        public SubLessonViewModel(int lessonSectionId)
        {
            _subLessonService = new SubLessonService(lessonSectionId);
            _ = LoadSubLessonsAsync();
        }

        public async Task LoadSubLessonsAsync()
        {
            try
            {
                var subLessons = await _subLessonService.GetSubLessonsBySectionAsync();
                SubLessons = new ObservableCollection<SubLesson>(subLessons);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

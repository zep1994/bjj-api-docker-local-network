using BjjTrainer.Models.Lessons;
using BjjTrainer.Services.Lessons;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace BjjTrainer.ViewModels
{
    public class LessonsViewModel : BaseViewModel
    {
        private readonly LessonService _lessonService;

        public ObservableCollection<Lesson> Lessons { get; } = [];

        public LessonsViewModel(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task LoadLessonsAsync()
        {
            var lessons = await _lessonService.GetAllLessons();
            foreach (var lesson in lessons)
            {
                Lessons.Add(lesson);
            }
        }
    }
}
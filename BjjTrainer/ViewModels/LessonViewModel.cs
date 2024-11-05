using MvvmHelpers;
using BjjTrainer.Models.Lessons;
using System.Collections.ObjectModel;
using BjjTrainer.Lessons.Services;

namespace BjjTrainer.ViewModels
{
    public class LessonsViewModel : BaseViewModel
    {
        private readonly LessonService _lessonService = new();

        private ObservableCollection<Lesson> _lessons;
        public ObservableCollection<Lesson> Lessons
        {
            get => _lessons;
            set => SetProperty(ref _lessons, value);
        }

        // Property to store the selected lesson
        private Lesson? _selectedLesson;
        public Lesson? SelectedLesson
        {
            get => _selectedLesson;
            set => SetProperty(ref _selectedLesson, value);
        }

        public LessonsViewModel()
        {
            Lessons = [];
        }

        public async Task LoadLessonsAsync()
        {
            var lessons = await _lessonService.GetAllLessons();
            Lessons.Clear();
            foreach (var lesson in lessons)
            {
                Lessons.Add(lesson);
            }
        }
    }
}
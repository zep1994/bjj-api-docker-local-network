using MvvmHelpers;
using BjjTrainer.Models.Lessons;
using System.Collections.ObjectModel;
using BjjTrainer.Services;

namespace BjjTrainer.ViewModels
{
    public class LessonsViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        public ObservableCollection<Lesson> Lessons { get; set; } = [];

        // Property to store the selected lesson
        private Lesson? _selectedLesson;
        public Lesson? SelectedLesson
        {
            get => _selectedLesson;
            set => SetProperty(ref _selectedLesson, value);
        }

        public LessonsViewModel(ApiService apiService)
        {
            _apiService = new ApiService();
            Lessons = [];
        }

        public async Task LoadLessonsAsync()
        {
            var lessons = await _apiService.GetLessonsAsync();
            Lessons.Clear();
            foreach (var lesson in lessons)
            {
                Lessons.Add(lesson);
            }
        }
    }
}
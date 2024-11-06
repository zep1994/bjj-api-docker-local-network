using BjjTrainer.Services;
using BjjTrainer.Models.Lessons;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BjjTrainer.ViewModels
{
    public class LessonSectionViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        public ObservableCollection<LessonSection> LessonSections { get; private set; }

        public LessonSectionViewModel(ApiService apiService)
        {
            _apiService = apiService;
            LessonSections = new ObservableCollection<LessonSection>();
        }

        public async Task LoadLessonSectionsAsync(int lessonId)
        {
            var sections = await _apiService.GetLessonSectionsAsync(lessonId);
            LessonSections.Clear();
            foreach (var section in sections)
            {
                LessonSections.Add(section);
            }
        }
    }
}

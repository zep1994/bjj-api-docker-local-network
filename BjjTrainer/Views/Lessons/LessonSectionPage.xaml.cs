using BjjTrainer.ViewModels;
using BjjTrainer.Models.Lessons;

namespace BjjTrainer.Views.Lessons
{
    public partial class LessonSectionPage : ContentPage
    {
        private readonly LessonSectionViewModel _viewModel;

        public LessonSectionPage(int lessonId, string lessonTitle)
        {
            InitializeComponent();
            _viewModel = new LessonSectionViewModel(lessonId, lessonTitle);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadLessonSectionsAsync();
        }

        public async void OnViewSubLessonClicked(object sender, EventArgs e)
        {
           
        }
    }
}

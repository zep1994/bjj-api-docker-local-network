using BjjTrainer.ViewModels.Lessons;

namespace BjjTrainer.Views.Lessons
{
    public partial class SubLessonPage : ContentPage
    {
        private readonly SubLessonViewModel _viewModel;

        public SubLessonPage(int lessonSectionId)
        {
            InitializeComponent();
            _viewModel = new SubLessonViewModel(lessonSectionId);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadSubLessonsAsync(); // Ensure data is loaded when the page appears
        }
    }
}

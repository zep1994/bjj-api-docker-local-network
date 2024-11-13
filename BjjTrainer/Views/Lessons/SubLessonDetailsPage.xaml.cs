using BjjTrainer.ViewModels;

namespace BjjTrainer.Views.Lessons
{
    public partial class SubLessonDetailsPage : ContentPage
    {
        private readonly SubLessonDetailsViewModel _viewModel;

        public SubLessonDetailsPage(int subLessonId)
        {
            InitializeComponent();

            // Initialize the ViewModel and set it as the BindingContext
            _viewModel = new SubLessonDetailsViewModel(subLessonId);
            BindingContext = _viewModel;
        }

        // Event handler for the "Back to Lessons" button
        private async void OnBackToLessonsClicked(object sender, EventArgs e)
        {
            // Navigate back to the previous page (usually the SubLessonPage)
            await Shell.Current.GoToAsync("..");
        }
    }
}

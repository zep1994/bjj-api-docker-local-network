using BjjTrainer.Models.Lessons;
using BjjTrainer.ViewModels;

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

        // Event handler for when the View Details button is clicked
        private async void OnViewSubLessonClicked(object sender, EventArgs e)
        {
            // Get the SubLesson object from the CommandParameter
            var button = (Button)sender;
            var selectedSubLesson = (SubLesson)button.CommandParameter;

            // Navigate to SubLessonDetailsPage
            await Navigation.PushAsync(new SubLessonDetailsPage(selectedSubLesson.Id));
        }
    }
}

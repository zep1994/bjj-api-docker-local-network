using BjjTrainer.ViewModels;

namespace BjjTrainer.Views.Lessons
{
    public partial class SubLessonDetailsPage : ContentPage
    {
        public SubLessonDetailsPage(int subLessonId)
        {
            InitializeComponent();
            BindingContext = new SubLessonDetailsViewModel(subLessonId);
        }
    }
}

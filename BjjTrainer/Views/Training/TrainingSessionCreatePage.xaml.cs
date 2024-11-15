using BjjTrainer.ViewModels.Training;

namespace BjjTrainer.Views.Training
{
    public partial class TrainingSessionCreatePage : ContentPage
    {
        public TrainingSessionCreatePage(TrainingSessionCreateViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void BackCommand(object sender, EventArgs e)
        {
            // Navigate back to the previous page (usually the SubLessonPage)
            await Shell.Current.GoToAsync("..");
        }
    }
}

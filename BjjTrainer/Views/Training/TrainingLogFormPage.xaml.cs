using BjjTrainer.ViewModels.TrainingGoals;

namespace BjjTrainer.Views.Training
{
    public partial class TrainingLogFormPage : ContentPage
    {
        public TrainingLogFormPage()
        {
            InitializeComponent();
            BindingContext = new TrainingLogFormViewModel();
        }

        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as TrainingLogFormViewModel;
            if (viewModel != null)
            {
                bool success = await viewModel.SubmitLogAsync();
                if (success)
                {
                    await Shell.Current.GoToAsync("//TrainingLogListPage");
                }
            }
        }
    }
}

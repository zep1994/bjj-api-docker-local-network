using BjjTrainer.ViewModels.Training;

namespace BjjTrainer.Views.Training
{
    public partial class TrainingSessionDetailPage : ContentPage
    {
        public TrainingSessionDetailPage(TrainingSessionDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
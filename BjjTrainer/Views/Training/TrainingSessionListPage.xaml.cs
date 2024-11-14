using BjjTrainer.ViewModels.Training;

namespace BjjTrainer.Views.Training
{
    public partial class TrainingSessionListPage : ContentPage
    {
        public TrainingSessionListPage(TrainingSessionListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
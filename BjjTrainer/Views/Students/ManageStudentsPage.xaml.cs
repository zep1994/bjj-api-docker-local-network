using BjjTrainer.ViewModels.Students;

namespace BjjTrainer.Views.Students
{
    public partial class ManageStudentsPage : ContentPage
    {
        public ManageStudentsPage(ManageStudentsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
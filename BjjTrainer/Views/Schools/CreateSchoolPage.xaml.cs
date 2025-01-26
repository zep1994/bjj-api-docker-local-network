using BjjTrainer.ViewModels.Schools;

namespace BjjTrainer.Views.Schools
{
    public partial class CreateSchoolPage : ContentPage
    {
        public CreateSchoolPage(CreateSchoolViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}

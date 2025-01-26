using BjjTrainer.ViewModels.Schools;

namespace BjjTrainer.Views.Schools
{
    [QueryProperty(nameof(SchoolId), "id")]
    public partial class UpdateSchoolPage : ContentPage
    {
        public int SchoolId { get; set; }

        public UpdateSchoolPage(UpdateSchoolViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is UpdateSchoolViewModel viewModel)
            {
                await viewModel.LoadSchoolAsync(SchoolId);
            }
        }
    }
}

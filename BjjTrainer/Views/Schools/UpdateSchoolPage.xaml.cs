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

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//CoachManagementPage");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is UpdateSchoolViewModel viewModel)
            {
                await viewModel.LoadSchoolAsync(SchoolId);
            }
        }

        public async void OnSaveClicked()
        {
            try
            {
                Console.WriteLine($"Test");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating school: {ex.Message}");
            }
        }
    }
}

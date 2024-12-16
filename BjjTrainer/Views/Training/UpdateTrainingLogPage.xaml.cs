using BjjTrainer.ViewModels.TrainingLogs;

namespace BjjTrainer.Views.Training
{
    public partial class UpdateTrainingLogPage : ContentPage
    {
        private readonly UpdateTrainingLogViewModel _viewModel;

        public UpdateTrainingLogPage(int logId)
        {
            InitializeComponent();
            _viewModel = new UpdateTrainingLogViewModel(logId);
            BindingContext = _viewModel;
        }

        private void OnCalendarIconTapped(object sender, EventArgs e)
        {
            // Show the hidden DatePicker
            DatePickerField.Focus();
        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            // Update the view model's Date and display value
            _viewModel.Date = e.NewDate;
            _viewModel.SelectedDateDisplay = e.NewDate.ToString("yyyy-MM-dd");
        }

        private async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            var success = await _viewModel.UpdateLogAsync();
            if (success)
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}

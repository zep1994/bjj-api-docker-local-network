using BjjTrainer.ViewModels.Events;
using Syncfusion.Maui.Scheduler;

namespace BjjTrainer.Views.Events
{
    public partial class CreateEventPage : ContentPage
    {
        private readonly CreateEventViewModel _viewModel;

        public CreateEventPage(SchedulerAppointment? appointment = null)
        {
            InitializeComponent();
            _viewModel = new CreateEventViewModel();

            // Populate fields if editing an existing appointment
            if (appointment != null)
            {
                _viewModel.Title = appointment.Subject;
                _viewModel.Description = appointment.Notes;
                _viewModel.StartDate = appointment.StartTime;
                _viewModel.EndDate = appointment.EndTime;
                _viewModel.IsAllDay = appointment.IsAllDay;
            }

            BindingContext = _viewModel;
        }

        private async void OnSaveEventClicked(object sender, EventArgs e)
        {
            var success = await _viewModel.SaveEventAsync();

            if (success)
            {
                await DisplayAlert("Success", "Event saved successfully.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Failed to save event.", "OK");
            }
        }

        private async void OnCancelEventClicked(object sender, EventArgs e)
        {
            // Navigate back without saving
            await Navigation.PopAsync();
        }
    }
}


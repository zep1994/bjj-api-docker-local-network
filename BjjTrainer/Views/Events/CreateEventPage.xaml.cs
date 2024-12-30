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
                _viewModel.StartTime = appointment.StartTime.TimeOfDay;  
                _viewModel.EndDate = appointment.EndTime;
                _viewModel.EndTime = appointment.EndTime.TimeOfDay;  
                _viewModel.IsAllDay = appointment.IsAllDay;
            }


            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is CalendarViewModel vm)
            {
                await vm.LoadAppointments();
            }
        }

        private async void OnSaveEventClicked(object sender, EventArgs e)
        {
            var success = await _viewModel.SaveEventAsync();

            if (success)
            {
                await DisplayAlert("Success", "Event saved successfully.", "OK");

                if (Application.Current.MainPage.Navigation.NavigationStack
                    .FirstOrDefault(x => x is CalendarPage) is CalendarPage calendarPage)
                {
                    if (calendarPage.BindingContext is CalendarViewModel calendarViewModel)
                    {
                        await calendarViewModel.LoadAppointments();
                    }
                }

                await Navigation.PushAsync(new CalendarPage());
            }
            else
            {
                await DisplayAlert("Error", "Failed to save event.", "OK");
            }
        }


        private async void OnCancelEventClicked(object sender, EventArgs e)
        {
            // Navigate back without saving
            await Navigation.PushAsync(new CalendarPage());
        }
    }
}


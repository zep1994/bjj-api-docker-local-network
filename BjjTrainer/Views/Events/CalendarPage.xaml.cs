using BjjTrainer.ViewModels.Events;
using Syncfusion.Maui.Scheduler;

namespace BjjTrainer.Views.Events;

public partial class CalendarPage : ContentPage
{
    private readonly CalendarViewModel _viewModel;

    public CalendarPage()
    {
        InitializeComponent();
        _viewModel = new CalendarViewModel();
        BindingContext = _viewModel;

        EventScheduler.AppointmentsSource = _viewModel.Events;
    }

    // Corrected Tapped Handler
    private async void OnSchedulerTapped(object sender, SchedulerTappedEventArgs e)
    {
        // Check if any appointment was tapped
        var appointment = e.Appointments?.FirstOrDefault() as SchedulerAppointment;

        if (appointment != null)
        {
            // Edit the existing event
            await Navigation.PushAsync(new CreateEventPage(appointment));
        }
        else
        {
            // Create a new event
            await Navigation.PushAsync(new CreateEventPage());
        }
    }

    private async void OnAppointmentDrop(object sender, AppointmentDropEventArgs e)
    {
        if (e.Appointment is SchedulerAppointment appointment)
        {
            await _viewModel.UpdateDroppedEventAsync(appointment);
        }
    }
}

using BjjTrainer.ViewModels.Events;
using Syncfusion.Maui.Scheduler;

namespace BjjTrainer.Views.Events;

public partial class CalendarPage : ContentPage
{
    private readonly CalendarViewModel _viewModel;

    public CalendarPage() => InitializeComponent();

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CalendarViewModel vm)
        {
            await vm.LoadAppointments();
        }
        EventScheduler.View = SchedulerView.Month;
    }

    private async void OnSchedulerTapped(object sender, SchedulerTappedEventArgs e)
    {
        var appointment = e.Appointments?.FirstOrDefault() as SchedulerAppointment;

        if (appointment != null && appointment.Id != null)
        {
            int eventId = Convert.ToInt32(appointment.Id);
            Console.WriteLine($"Navigating to ShowEventPage with EventId: {eventId}");

            if (eventId > 0)  // Only navigate if the eventId is valid
            {
                await Navigation.PushAsync(new ShowEventPage(eventId));
            }
            else
            {
                Console.WriteLine("Invalid eventId detected. Navigation canceled.");
            }
        }
        else
        {
            await Navigation.PushAsync(new CreateEventPage());
        }
    }

    private async void OnAppointmentDrop(object sender, AppointmentDropEventArgs e)
    {
        if (e.Appointment is SchedulerAppointment appointment
            && BindingContext is CalendarViewModel vm)
        {
            await vm.UpdateDroppedEventAsync(appointment);
        }
    }

}

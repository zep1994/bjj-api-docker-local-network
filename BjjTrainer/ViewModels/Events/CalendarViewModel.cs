using System.Collections.ObjectModel;
using BjjTrainer.Models.DTO.Events;
using BjjTrainer.Services.Events;
using MvvmHelpers;
using Syncfusion.Maui.Scheduler;

namespace BjjTrainer.ViewModels.Events
{
    public partial class CalendarViewModel : BaseViewModel
    {
        private readonly EventService _eventService;

        public ObservableCollection<SchedulerAppointment> Appointments { get; set; } = [];


        public CalendarViewModel()
        {
            _eventService = new EventService();
            LoadAppointments();
        }

        // LOAD EVENT
        public async Task LoadAppointments()
        {
            var userId = Preferences.Get("UserId", string.Empty);
            var schoolId = Preferences.Get("SchoolId", 0);
            var userRole = Preferences.Get("UserRole", "Student");

            var events = await _eventService.GetAllUserEventsAsync(userId);

            if (events != null)
            {
                Appointments.Clear();
                foreach (var calendarEvent in events)
                {
                    var startTime = TimeSpan.TryParse(calendarEvent.StartTime, out var parsedStart)
                        ? parsedStart
                        : TimeSpan.Zero;

                    var endTime = TimeSpan.TryParse(calendarEvent.EndTime, out var parsedEnd)
                        ? parsedEnd
                        : TimeSpan.Zero;

                    var appointment = new SchedulerAppointment
                    {
                        Id = calendarEvent.Id,  // Ensure correct eventId is set
                        Subject = calendarEvent.Title,
                        Background = Colors.BlueViolet,
                        IsAllDay = calendarEvent.IsAllDay,
                        StartTime = calendarEvent.StartDate + startTime,
                        EndTime = calendarEvent.EndDate + endTime
                    };

                    if (appointment.StartTime == appointment.EndTime)
                    {
                        appointment.EndTime = appointment.StartTime.Add(TimeSpan.FromMinutes(1));
                    }

                    Appointments.Add(appointment);
                }

            }
        }

        // DROP EVENT
        public async Task UpdateDroppedEventAsync(SchedulerAppointment appointment)
        {
            var updatedEvent = new CalendarEventDto
            {
                Id = appointment.Id != null ? Convert.ToInt32(appointment.Id) : 0,
                Title = appointment.Subject,
                Description = appointment.Notes,
                StartDate = appointment.StartTime,
                EndDate = appointment.EndTime,
                IsAllDay = appointment.IsAllDay,
                ApplicationUserId = Preferences.Get("UserId", string.Empty)
            };
            await _eventService.UpdateEventAsync(updatedEvent.Id, updatedEvent);
            await LoadAppointments();
        }
    }
}

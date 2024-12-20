using System.Collections.ObjectModel;
using BjjTrainer.Models.DTO.Events;
using BjjTrainer.Services.Events;
using MvvmHelpers;
using Syncfusion.Maui.Scheduler;

namespace BjjTrainer.ViewModels.Events
{
    public class CalendarViewModel : BaseViewModel
    {
        private readonly EventService _eventService;

        public ObservableCollection<SchedulerAppointment> Events { get; set; }

        public CalendarViewModel()
        {
            _eventService = new EventService();
            Events = new ObservableCollection<SchedulerAppointment>();

            Task.Run(async () => await LoadEventsAsync());
        }

        public async Task LoadEventsAsync()
        {
            IsBusy = true;

            try
            {
                var userId = Preferences.Get("UserId", string.Empty);
                var events = await _eventService.GetAllUserEventsAsync(userId);

                Events.Clear();

                foreach (var evt in events)
                {
                    Events.Add(new SchedulerAppointment
                    {
                        StartTime = evt.StartDate,
                        EndTime = evt.EndDate,
                        Subject = evt.Title,
                        Notes = evt.Description,
                        IsAllDay = evt.IsAllDay
                    });
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

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
            await LoadEventsAsync();
        }
    }
}

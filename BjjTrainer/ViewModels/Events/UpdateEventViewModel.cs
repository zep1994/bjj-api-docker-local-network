using BjjTrainer.Models.DTO.Events;
using BjjTrainer.Services.Events;
using MvvmHelpers;

namespace BjjTrainer.ViewModels.Events
{
    public partial class UpdateEventViewModel : BaseViewModel
    {
        private readonly EventService _eventService;
        public int EventId { get; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public UpdateEventViewModel(int eventId)
        {
            EventId = eventId;
            _eventService = new EventService();
        }

        public async Task LoadEventDetailsAsync()
        {
            try
            {
                var eventDetails = await _eventService.GetEventByIdAsync(EventId);
                Console.WriteLine($"Loaded Event for Update: {eventDetails.Title}");

                Title = eventDetails.Title;
                Description = eventDetails.Description;
                StartDate = eventDetails.StartDate;
                EndDate = eventDetails.EndDate;

                // Parse TimeSpan from string fields
                StartTime = TimeSpan.TryParse(eventDetails.StartTime, out var start)
                    ? start
                    : TimeSpan.Zero;

                EndTime = TimeSpan.TryParse(eventDetails.EndTime, out var end)
                    ? end
                    : TimeSpan.Zero;

                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(StartDate));
                OnPropertyChanged(nameof(EndDate));
                OnPropertyChanged(nameof(StartTime));
                OnPropertyChanged(nameof(EndTime));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading event details for update: {ex.Message}");
            }
        }

        public async Task<bool> SaveEventAsync()
        {
            var updatedEvent = new CalendarEventDto
            {
                Id = EventId,
                Title = Title,
                Description = Description,
                StartDate = StartDate,
                StartTime = StartTime.ToString(@"hh\:mm"),
                EndDate = EndDate,
                EndTime = EndTime.ToString(@"hh\:mm")
            };

            return await _eventService.UpdateEventAsync(EventId, updatedEvent);
        }
    }
}

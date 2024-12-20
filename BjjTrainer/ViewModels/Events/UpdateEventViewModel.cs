using BjjTrainer.Models.DTO.Events;
using BjjTrainer.Services.Events;
using MvvmHelpers;

namespace BjjTrainer.ViewModels.Events
{
    public class UpdateEventViewModel : BaseViewModel
    {
        private readonly EventService _eventService;
        public int EventId { get; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public UpdateEventViewModel(int eventId)
        {
            EventId = eventId;
            _eventService = new EventService();
            Task.Run(async () => await LoadEventDetailsAsync());
        }

        public async Task LoadEventDetailsAsync()
        {
            var eventDetails = await _eventService.GetEventByIdAsync(EventId);
            Title = eventDetails.Title;
            Description = eventDetails.Description;
            StartDate = eventDetails.StartDate;
            EndDate = eventDetails.EndDate;
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(StartDate));
            OnPropertyChanged(nameof(EndDate));
        }

        public async Task<bool> SaveEventAsync()
        {
            var updatedEvent = new CalendarEventDto
            {
                Title = Title,
                Description = Description,
                StartDate = StartDate,
                EndDate = EndDate,
                ApplicationUserId = Preferences.Get("UserId", string.Empty)
            };

            return await _eventService.UpdateEventAsync(EventId, updatedEvent);
        }

        public async Task<bool> DeleteEventAsync()
        {
            return await _eventService.DeleteEventAsync(EventId);
        }
    }

}

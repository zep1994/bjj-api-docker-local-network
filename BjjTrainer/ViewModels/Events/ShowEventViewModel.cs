using BjjTrainer.Services.Events;
using MvvmHelpers;


namespace BjjTrainer.ViewModels.Events
{
    public class ShowEventViewModel : BaseViewModel
    {
        private readonly EventService _eventService;
        public int EventId { get; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string FormattedDate { get; set; }
        public string FormattedEndDate { get; set; }


        public ShowEventViewModel(int eventId)
        {
            EventId = eventId;
            _eventService = new EventService();
            Task.Run(async () => await LoadEventDetailsAsync());
        }

        public async Task LoadEventDetailsAsync()
        {
            try
            {
                var eventDetails = await _eventService.GetEventByIdAsync(EventId);
                Console.WriteLine($"Loaded Event Details: {eventDetails.Title}"); // Debugging output
                Title = eventDetails.Title;
                Description = eventDetails.Description;
                FormattedDate = eventDetails.FormattedDate;
                FormattedEndDate = eventDetails.FormattedEndDate;
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(FormattedDate));
                OnPropertyChanged(nameof(FormattedEndDate));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading event details: {ex.Message}");
            }
        }
    }
}

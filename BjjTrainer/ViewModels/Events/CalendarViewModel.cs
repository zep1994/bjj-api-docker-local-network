using BjjTrainer.Models.DTO.Events;
using BjjTrainer.Services.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BjjTrainer.ViewModels.Events
{
    public class CalendarViewModel : BindableObject
    {
        private readonly EventService _eventService;
        private int _year;
        private int _month;

        public ObservableCollection<CalendarEventDto> Events { get; set; } = new ObservableCollection<CalendarEventDto>();

        public ICommand ChangeYearMonthCommand { get; }

        public CalendarViewModel()
        {
            _eventService = new EventService(); // Or inject via dependency injection.
            _year = DateTime.Now.Year;
            _month = DateTime.Now.Month;

            // Command to handle navigation
            ChangeYearMonthCommand = new Command<string>(ChangeYearMonth);

            // Temporary hardcoded events for testing
            Events.Add(new CalendarEventDto
            {
                Title = "Sample Event 1",
                Description = "This is a sample description for event 1.",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            });
            Events.Add(new CalendarEventDto
            {
                Title = "Sample Event 2",
                Description = "This is another sample description for event 2.",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3)
            });

            // Load actual events from the API
            LoadEvents();
        }

        private async void LoadEvents()
        {
            try
            {
                var userId = Preferences.Get("UserId", string.Empty);
                if (string.IsNullOrWhiteSpace(userId))
                {
                    throw new Exception("UserId not found in preferences.");
                }

                var events = await _eventService.GetUserEventsAsync(userId, _year, _month);
                foreach (var calendarEvent in events)
                {
                    Console.WriteLine($"Title: {calendarEvent.Title}, Start: {calendarEvent.StartDate}, End: {calendarEvent.EndDate}");
                }

                Events.Clear();

                foreach (var calendarEvent in events)
                {
                    Events.Add(calendarEvent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load events: {ex.Message}");
                // You can add UI feedback for errors here.
            }
        }

        private void ChangeYearMonth(string direction)
        {
            if (direction == "Previous")
            {
                _month--;
                if (_month < 1)
                {
                    _month = 12;
                    _year--;
                }
            }
            else if (direction == "Next")
            {
                _month++;
                if (_month > 12)
                {
                    _month = 1;
                    _year++;
                }
            }

            LoadEvents();
        }
    }
}

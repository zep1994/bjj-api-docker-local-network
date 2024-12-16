using BjjTrainer.Models.DTO.Events;
using BjjTrainer.Models.Util;
using BjjTrainer.Services.Events;
using System.Collections.ObjectModel;

namespace BjjTrainer.ViewModels.Events
{
    public partial class CalendarViewModel : BindableObject
    {
        private readonly EventService _eventService;
        private DateTime _selectedDate;

        public ObservableCollection<DayEvents> Days { get; set; } = new ObservableCollection<DayEvents>();
        public bool NoEventsThisWeek { get; set; }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                Task.Run(async () => await LoadEventsForWeekAsync());
            }
        }

        public bool IsBusy { get; private set; }

        public CalendarViewModel()
        {
            _eventService = new EventService();
            _selectedDate = DateTime.Now; // Default to the current date
            Task.Run(async () => await LoadEventsForWeekAsync());
        }

        public async Task LoadEventsForWeekAsync()
        {
            IsBusy = true;

            try
            {
                var userId = Preferences.Get("UserId", string.Empty);
                if (string.IsNullOrWhiteSpace(userId))
                    throw new Exception("UserId not found in preferences.");

                var startOfWeek = SelectedDate.AddDays(-(int)SelectedDate.DayOfWeek);
                var endOfWeek = startOfWeek.AddDays(7);

                var events = await _eventService.GetAllUserEventsAsync(userId);

                Days.Clear();

                for (var date = startOfWeek; date < endOfWeek; date = date.AddDays(1))
                {
                    var dayEvents = new DayEvents
                    {
                        DayHeader = date.ToString("dddd, MMM dd"),
                        Events = new ObservableCollection<CalendarEventDto>(
                            events.Where(e => e.StartDate.Date == date.Date).ToList())
                    };

                    dayEvents.HasEvents = dayEvents.Events.Any();
                    Days.Add(dayEvents);
                }

                NoEventsThisWeek = !Days.Any(d => d.HasEvents);
                OnPropertyChanged(nameof(Days));
                OnPropertyChanged(nameof(NoEventsThisWeek));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading events: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void NavigateToPreviousWeek()
        {
            SelectedDate = SelectedDate.AddDays(-7);
        }

        public void NavigateToNextWeek()
        {
            SelectedDate = SelectedDate.AddDays(7);
        }
    }
}

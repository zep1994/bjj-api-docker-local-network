using BjjTrainer.Models.DTO.Events;
using BjjTrainer.Services.Events;
using BjjTrainer.Services.Users;
using MvvmHelpers;

namespace BjjTrainer.ViewModels.Events
{
    public class CreateEventViewModel : BaseViewModel
    {
        private readonly EventService _eventService;
        private readonly UserService _userService;

        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now.Date.AddDays(0);
        public TimeSpan StartTime { get; set; } = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(1));
        public DateTime EndDate { get; set; } = DateTime.Now.Date.AddDays(1);
        public TimeSpan EndTime { get; set; } = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(1));
        public bool IsAllDay { get; set; } = false;

        public CreateEventViewModel()
        {
            _eventService = new EventService();
            _userService = new UserService();
        }

        public async Task<bool> SaveEventAsync()
        {
            IsBusy = true;

            try
            {
                var userRole = Preferences.Get("UserRole", "Student");
                var schoolId = userRole == "Coach" ? (int?)Preferences.Get("SchoolId", 0) : null;

                var newEvent = new CalendarEventCreateDTO
                {
                    Title = Title ?? string.Empty,
                    Description = Description ?? string.Empty,
                    StartDate = StartDate,
                    StartTime = StartTime,
                    EndDate = EndDate < StartDate ? StartDate : EndDate,
                    EndTime = EndTime,
                    IsAllDay = IsAllDay,
                    ApplicationUserId = Preferences.Get("UserId", string.Empty),
                    SchoolId = schoolId  // Fixed casting issue
                };

                string endpoint = "calendar/events/create";
                return await _eventService.CreateEventAsync(endpoint, newEvent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving event: {ex.Message}");
                return false;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

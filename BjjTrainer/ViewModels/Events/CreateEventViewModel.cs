using BjjTrainer.Models.DTO.Events;
using BjjTrainer.Services.Events;
using MvvmHelpers;

namespace BjjTrainer.ViewModels.Events
{
    public class CreateEventViewModel : BaseViewModel
    {
        private readonly EventService _eventService;

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddHours(1);
        public bool IsAllDay { get; set; }

        public CreateEventViewModel()
        {
            _eventService = new EventService();
        }

        public async Task<bool> SaveEventAsync()
        {
            IsBusy = true;

            try
            {
                var newEvent = new CalendarEventCreateDTO
                {
                    Title = Title,
                    Description = Description,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    IsAllDay = IsAllDay,
                    ApplicationUserId = Preferences.Get("UserId", string.Empty)
                };

                return await _eventService.CreateEventAsync(newEvent);
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

using BjjTrainer.Models.DTO.Coaches;
using BjjTrainer.Services.Coaches;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BjjTrainer.ViewModels.Coaches
{
    public partial class CoachEventDetailViewModel : ObservableObject
    {
        private readonly CoachService _coachService;

        [ObservableProperty]
        private CoachEventDto selectedEvent = new(); // Ensure initialization to prevent binding issues

        public CoachEventDetailViewModel()
        {
            _coachService = new CoachService();
        }

        public async Task Initialize(int eventId)
        {
            await LoadEventDetails(eventId);
        }

        [RelayCommand]
        public async Task LoadEventDetails(int eventId)
        {
            try
            {
                var eventDetails = await _coachService.GetEventDetailsAsync(eventId);
                if (eventDetails != null)
                {
                    SelectedEvent = eventDetails;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading event details: {ex.Message}");
            }
        }
    }
}

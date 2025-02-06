using BjjTrainer.Models.DTO.Events;
using BjjTrainer.Services.Coaches;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BjjTrainer.ViewModels.Coaches
{
    public partial class CoachViewModel : ObservableObject
    {
        private readonly CoachService _coachService;

        public CoachViewModel(CoachService coachService)
        {
            _coachService = coachService;
        }

        [ObservableProperty]
        private ObservableCollection<PastEventDetails> pastEvents;

        [ObservableProperty]
        private CalendarEventDto selectedEvent;

        [RelayCommand]
        public async Task LoadPastEvents(int schoolId)
        {
            try
            {
                var events = await _coachService.GetPastEventsWithDetailsAsync(schoolId);
                PastEvents = new ObservableCollection<PastEventDetails>(events);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading past events: {ex.Message}");
            }
        }
    }
}

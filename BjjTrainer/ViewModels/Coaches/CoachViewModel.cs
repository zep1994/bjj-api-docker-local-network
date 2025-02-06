using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BjjTrainer.Models.DTO.Events;
using BjjTrainer.Services.Coaches;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
        private ObservableCollection<CalendarEventDto> pastEvents;

        [ObservableProperty]
        private ObservableCollection<UserCalendarEvent> eventCheckIns;

        [ObservableProperty]
        private CalendarEventDto selectedEvent;

        [RelayCommand]
        public async Task LoadPastEvents(int schoolId)
        {
            var events = await _coachService.GetPastEventsAsync(schoolId);
            PastEvents = new ObservableCollection<CalendarEventDto>(events);
        }

        [RelayCommand]
        public async Task LoadEventCheckIns(int eventId)
        {
            var checkIns = await _coachService.GetEventCheckInsAsync(eventId);
            EventCheckIns = new ObservableCollection<UserCalendarEvent>(checkIns);
        }
    }
}

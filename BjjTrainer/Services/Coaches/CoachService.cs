using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BjjTrainer.Models.DTO.Events;

namespace BjjTrainer.Services.Coaches
{
    public class CoachService : ApiService
    {
        public async Task<List<CalendarEventDto>> GetPastEventsAsync(int schoolId)
        {
            return await HttpClient.GetFromJsonAsync<List<CalendarEventDto>>($"coach/events/{schoolId}");
        }

        public async Task<List<UserCalendarEvent>> GetEventCheckInsAsync(int eventId)
        {
            return await HttpClient.GetFromJsonAsync<List<UserCalendarEvent>>($"coach/event-checkins/{eventId}");
        }
    }
}

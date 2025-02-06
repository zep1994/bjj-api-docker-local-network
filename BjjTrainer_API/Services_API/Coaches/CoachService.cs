using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Calendars;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Services_API.Coaches
{
    public class CoachService
    {
        private readonly ApplicationDbContext _context;

        public CoachService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CalendarEvent>> GetPastEventsForSchool(int schoolId)
        {
            return await _context.CalendarEvents
                .Where(e => e.SchoolId == schoolId && e.EndDate < DateTime.UtcNow)
                .OrderByDescending(e => e.EndDate)
                .ToListAsync();
        }

        public async Task<List<CalendarEventCheckIn>> GetCheckInsForEvent(int eventId)
        {
            return await _context.CalendarEventCheckIns
                .Where(c => c.CalendarEventId == eventId)
                .Include(c => c.User)
                .ToListAsync();
        }
    }
}

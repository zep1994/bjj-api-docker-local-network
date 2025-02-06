using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Calendars;
using BjjTrainer_API.Models.DTO.Calendars;
using BjjTrainer_API.Models.DTO.Moves;
using BjjTrainer_API.Models.DTO.TrainingLogDTOs;
using BjjTrainer_API.Models.Users;
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

        public async Task<List<PastEventDetailsDto>> GetPastEventsWithLogs(string coachId, int schoolId)
        {
            // Ensure only coaches can access this data
            var coach = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == coachId && u.Role == UserRole.Coach);

            if (coach == null || coach.SchoolId != schoolId)
            {
                throw new UnauthorizedAccessException("You are not authorized to access these events.");
            }

            var events = await _context.CalendarEvents
                .Where(e => e.SchoolId == schoolId && e.EndDate < DateTime.UtcNow)
                .Include(e => e.TrainingLog)
                .ThenInclude(tl => tl.TrainingLogMoves)
                .ThenInclude(tlm => tlm.Move)
                .Include(e => e.CalendarEventCheckIns)
                .ThenInclude(ci => ci.User)
                .ToListAsync();

            var result = events.Select(ev => new PastEventDetailsDto
            {
                Event = ev,
                CheckIns = ev.CalendarEventCheckIns.Select(c => new CheckInDetailsDto
                {
                    Id = c.User.Id,
                    UserName = c.User.UserName,
                    CheckInTime = c.CheckInTime
                }).ToList(),
                TrainingLog = ev.TrainingLog != null ? new TrainingLogDto
                {
                    Id = ev.TrainingLog.Id,
                    ApplicationUserId = ev.TrainingLog.ApplicationUserId,
                    Date = ev.TrainingLog.Date,
                    TrainingTime = ev.TrainingLog.TrainingTime,
                    RoundsRolled = ev.TrainingLog.RoundsRolled,
                    Submissions = ev.TrainingLog.Submissions,
                    Taps = ev.TrainingLog.Taps,
                    Notes = ev.TrainingLog.Notes,
                    SelfAssessment = ev.TrainingLog.SelfAssessment,
                    IsCoachLog = ev.TrainingLog.IsCoachLog,
                    Moves = ev.TrainingLog.TrainingLogMoves.Select(tlm => new LogMoveDto
                    {
                        Id = tlm.Move.Id,
                        Name = tlm.Move.Name
                    }).ToList()
                } : null
            }).ToList();

            return result;
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

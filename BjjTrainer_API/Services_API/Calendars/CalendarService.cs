using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Calendars;
using BjjTrainer_API.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Services_API.Calendars
{
    public class CalendarService
    {
        private readonly ApplicationDbContext _context;

        public CalendarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CalendarEvent?> GetEventByIdAsync(int eventId)
        {
            return await _context.CalendarEvents
                .Where(e => e.Id == eventId)
                .Select(e => new CalendarEvent
                {
                    Id = e.Id,
                    StartDate = e.StartDate,
                    StartTime = e.StartTime,
                    EndDate = e.EndDate,
                    EndTime = e.EndTime,
                    Title = e.Title
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CalendarEvent> CreateEventAsync(string userId, CalendarEvent model)
        {
            var user = await _context.ApplicationUsers
                .Include(u => u.School)
                .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new Exception("User not found.");

            var calendarEvent = new CalendarEvent
            {
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                StartTime = model.StartTime,
                EndDate = model.EndDate,
                EndTime = model.EndTime,
                IsAllDay = model.IsAllDay,
                SchoolId = model.SchoolId
            };

            _context.CalendarEvents.Add(calendarEvent);
            await _context.SaveChangesAsync();

            _context.CalendarEventUsers.Add(new CalendarEventUser
            {
                CalendarEventId = calendarEvent.Id,
                UserId = user.Id
            });

            await _context.SaveChangesAsync();
            return calendarEvent;
        }


        // Create event for a coach (linked to school)
        public async Task<CalendarEvent> CreateCoachEventAsync(string userId, CalendarEvent model)
        {
            var user = await _context.ApplicationUsers
                .Include(u => u.School)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Role != UserRole.Coach || user.SchoolId == null)
                throw new Exception("Only coaches with a school can create events.");

            var calendarEvent = new CalendarEvent
            {
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                StartTime = model.StartTime,
                EndDate = model.EndDate,
                EndTime = model.EndTime,
                SchoolId = user.SchoolId
            };

            _context.CalendarEvents.Add(calendarEvent);
            await _context.SaveChangesAsync();

            _context.CalendarEventUsers.Add(new CalendarEventUser
            {
                CalendarEventId = calendarEvent.Id,
                UserId = user.Id
            });

            await _context.SaveChangesAsync();
            return calendarEvent;
        }

        // Create personal event for a student
        public async Task<CalendarEvent> CreateStudentEventAsync(string userId, CalendarEvent model)
        {
            var user = await _context.ApplicationUsers.FindAsync(userId);
            if (user == null || user.Role != UserRole.Student)
                throw new Exception("Only students can create personal events.");

            var calendarEvent = new CalendarEvent
            {
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            _context.CalendarEvents.Add(calendarEvent);
            await _context.SaveChangesAsync();

            // Automatically link the student to the event
            _context.CalendarEventUsers.Add(new CalendarEventUser
            {
                CalendarEventId = calendarEvent.Id,
                UserId = user.Id
            });

            await _context.SaveChangesAsync();
            return calendarEvent;
        }

        // GET USER SCHOOL
        public async Task<int?> GetUserSchoolIdAsync(string userId)
        {
            return await _context.ApplicationUsers
                .Where(u => u.Id == userId)
                .Select(u => u.SchoolId)
                .FirstOrDefaultAsync();
        }

        // Update an existing event
        public async Task UpdateEventAsync(int eventId, CalendarEvent model, string userId)
        {
            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new Exception("User not found.");

            var calendarEvent = await _context.CalendarEvents
                .FirstOrDefaultAsync(e => e.Id == eventId) ?? throw new Exception("Event not found.");

            // Restrict students from updating school-wide events
            if (user.Role == UserRole.Student && calendarEvent.SchoolId != null)
            {
                throw new Exception("Students can only update their personal events.");
            }

            // Allow coaches to update school-wide events within their school
            if (user.Role == UserRole.Coach && calendarEvent.SchoolId != user.SchoolId)
            {
                throw new Exception("You can only update events for your school.");
            }

            // Update event details
            calendarEvent.Title = model.Title;
            calendarEvent.Description = model.Description;
            calendarEvent.StartDate = model.StartDate;
            calendarEvent.StartTime = model.StartTime;
            calendarEvent.EndDate = model.EndDate;
            calendarEvent.EndTime = model.EndTime;
            calendarEvent.IsAllDay = model.IsAllDay;

            await _context.SaveChangesAsync();
        }


        // DELETE
        public async Task DeleteEventAsync(int eventId, string userId)
        {
            var user = await _context.ApplicationUsers.FindAsync(userId);
            var calendarEvent = await _context.CalendarEvents
                .Include(e => e.CalendarEventUsers)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (calendarEvent == null)
                throw new Exception("Event not found.");

            if (calendarEvent.EndDate < DateTime.UtcNow)
                throw new Exception("Cannot delete past events.");

            if (user != null && user.Role == UserRole.Coach)
            {
                _context.CalendarEventUsers.RemoveRange(calendarEvent.CalendarEventUsers);
                _context.CalendarEvents.Remove(calendarEvent);
            }
            else
            {
                var userEvent = calendarEvent.CalendarEventUsers
                    .FirstOrDefault(ceu => ceu.UserId == userId && ceu.User.Role == UserRole.Student);

                if (userEvent != null)
                {
                    _context.CalendarEventUsers.Remove(userEvent);

                    if (!calendarEvent.CalendarEventUsers.Any())
                    {
                        _context.CalendarEvents.Remove(calendarEvent);
                    }
                }
                else
                {
                    throw new Exception("You can only delete your personal events.");
                }
            }

            await _context.SaveChangesAsync();
        }


        // Allow a user to join an existing event
        public async Task JoinEventAsync(int eventId, string userId)
        {
            var user = await _context.ApplicationUsers.FindAsync(userId);
            var calendarEvent = await _context.CalendarEvents
                .Include(e => e.School)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (calendarEvent == null)
                throw new Exception("Event not found.");

            // Prevent joining events from different schools for students
            if (user.SchoolId != calendarEvent.SchoolId)
                throw new Exception("You can only join events from your school.");

            var existingEventUser = await _context.CalendarEventUsers
                .FirstOrDefaultAsync(eu => eu.CalendarEventId == eventId && eu.UserId == userId);

            if (existingEventUser != null)
                throw new Exception("You have already joined this event.");

            _context.CalendarEventUsers.Add(new CalendarEventUser
            {
                CalendarEventId = eventId,
                UserId = userId
            });

            await _context.SaveChangesAsync();
        }

        // Check in to an event after joining
        public async Task CheckInEventAsync(int eventId, string userId)
        {
            var eventUser = await _context.CalendarEventUsers
                .FirstOrDefaultAsync(eu => eu.CalendarEventId == eventId && eu.UserId == userId);

            if (eventUser == null)
                throw new Exception("You must join the event before checking in.");

            if (eventUser.IsCheckedIn)
                throw new Exception("You have already checked in.");

            eventUser.IsCheckedIn = true;

            _context.CalendarEventCheckIns.Add(new CalendarEventCheckIn
            {
                CalendarEventId = eventId,
                UserId = userId,
                CheckInTime = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

        // Retrieve all events for a user
        public async Task<List<CalendarEvent>> GetEventsForUserAsync(string userId)
        {
            var events = await _context.CalendarEventUsers
                .Where(eu => eu.UserId == userId)
                .Select(eu => eu.CalendarEvent)
                .ToListAsync();

            return events;
        }
    }
}
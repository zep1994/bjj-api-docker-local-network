using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Calendars;
using BjjTrainer_API.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
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

        // Create event for a coach (linked to school)
        public async Task<CalendarEvent> CreateCoachEventAsync(string userId, CalendarEvent model)
        {
            var user = await _context.ApplicationUsers
                .Include(u => u.School)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || !user.IsCoach || user.SchoolId == null)
                throw new Exception("Only coaches with a school can create events.");

            var calendarEvent = new CalendarEvent
            {
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                SchoolId = user.SchoolId 
            };

            _context.CalendarEvents.Add(calendarEvent);
            await _context.SaveChangesAsync();

            // Automatically enroll the coach and students in the event
            _context.CalendarEventUsers.Add(new CalendarEventUser
            {
                CalendarEventId = calendarEvent.Id,
                UserId = user.Id
            });

            var students = await _context.ApplicationUsers
                .Where(u => u.SchoolId == user.SchoolId && !u.IsCoach)
                .ToListAsync();

            foreach (var student in students)
            {
                _context.CalendarEventUsers.Add(new CalendarEventUser
                {
                    CalendarEventId = calendarEvent.Id,
                    UserId = student.Id
                });
            }

            await _context.SaveChangesAsync();
            return calendarEvent;
        }

        // Create personal event for a student
        public async Task<CalendarEvent> CreateStudentEventAsync(string userId, CalendarEvent model)
        {
            var user = await _context.ApplicationUsers.FindAsync(userId);
            if (user == null || user.IsCoach)
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

        // Update an existing event
        public async Task UpdateEventAsync(int eventId, CalendarEvent model)
        {
            var calendarEvent = await _context.CalendarEvents.FindAsync(eventId);
            if (calendarEvent == null)
                throw new Exception("Event not found.");

            calendarEvent.Title = model.Title;
            calendarEvent.Description = model.Description;
            calendarEvent.StartDate = model.StartDate;
            calendarEvent.EndDate = model.EndDate;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int eventId, string userId)
        {
            var user = await _context.ApplicationUsers.FindAsync(userId);
            var calendarEvent = await _context.CalendarEvents
                .Include(e => e.CalendarEventUsers)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (calendarEvent == null)
                throw new Exception("Event not found.");

            // Prevent deletion of past events
            if (calendarEvent.EndDate < DateTime.UtcNow)
                throw new Exception("Cannot delete past events.");

            // If user is a coach, allow full deletion of event
            if (user != null && user.IsCoach)
            {
                _context.CalendarEventUsers.RemoveRange(calendarEvent.CalendarEventUsers);
                _context.CalendarEvents.Remove(calendarEvent);
            }
            else
            {
                // Students can only delete their personal events
                var userEvent = calendarEvent.CalendarEventUsers
                    .FirstOrDefault(ceu => ceu.UserId == userId && !ceu.User.IsCoach);

                if (userEvent != null)
                {
                    _context.CalendarEventUsers.Remove(userEvent);

                    // If the event is now empty (no users), delete the event itself
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
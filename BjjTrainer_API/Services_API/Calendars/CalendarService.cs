using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Calendars;
using BjjTrainer_API.Models.DTO;
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

        public async Task<CalendarEventDto> CreateEventAsync(CalendarEventCreateDTO newEventDto)
        {
            var calendarEvent = new CalendarEvent
            {
                ApplicationUserId = newEventDto.ApplicationUserId,
                Title = newEventDto.Title,
                Description = newEventDto.Description,
                StartDate = newEventDto.StartDate,
                EndDate = newEventDto.EndDate
            };

            _context.CalendarEvents.Add(calendarEvent);
            await _context.SaveChangesAsync();

            return new CalendarEventDto
            {
                Id = calendarEvent.Id,
                Title = calendarEvent.Title,
                Description = calendarEvent.Description,
                StartDate = calendarEvent.StartDate,
                EndDate = calendarEvent.EndDate
            };
        }

        public async Task<CalendarEventDto> GetEventByIdAsync(int id)
        {
            var calendarEvent = await _context.CalendarEvents.FindAsync(id);
            if (calendarEvent == null)
                return null;

            return new CalendarEventDto
            {
                Id = calendarEvent.Id,
                Title = calendarEvent.Title,
                Description = calendarEvent.Description,
                StartDate = calendarEvent.StartDate,
                EndDate = calendarEvent.EndDate
            };
        }

        public async Task<List<CalendarEventDto>> GetUserEventsAsync(string userId, int year, int month)
        {
            try
            {
                var events = await _context.CalendarEvents
                    .Where(e => e.ApplicationUserId == userId
                                && e.StartDate.HasValue && e.StartDate.Value.Year == year
                                && e.StartDate.Value.Month == month)
                    .ToListAsync();

                var eventDtos = events.Select(e => new CalendarEventDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    // Make sure to access the Year and Month only if StartDate is not null
                    StartDate = e.StartDate.HasValue ? e.StartDate.Value : (DateTime?)null,
                    EndDate = e.EndDate.HasValue ? e.EndDate.Value : (DateTime?)null
                }).ToList();

                return eventDtos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving events: {ex.Message}");
            }
        }


        // Update an existing event
        public async Task<CalendarEvent> UpdateEvent(CalendarEvent updatedEvent)
        {
            var existingEvent = await _context.CalendarEvents.FindAsync(updatedEvent.Id);
            if (existingEvent == null)
            {
                return null; // Event not found
            }

            existingEvent.Title = updatedEvent.Title;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.StartDate = updatedEvent.StartDate;
            existingEvent.EndDate = updatedEvent.EndDate;

            await _context.SaveChangesAsync();
            return existingEvent;
        }

        // Delete an event
        public async Task<bool> DeleteEvent(int eventId)
        {
            var eventToDelete = await _context.CalendarEvents.FindAsync(eventId);
            if (eventToDelete == null)
            {
                return false; // Event not found
            }

            _context.CalendarEvents.Remove(eventToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
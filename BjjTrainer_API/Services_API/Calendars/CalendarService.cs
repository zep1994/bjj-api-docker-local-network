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

        public async Task<List<CalendarEventDto>> GetUserEventsAsync(string userId)
        {
            try
            {
                var events = await _context.CalendarEvents
                    .Where(e => e.ApplicationUserId == userId
                                && e.StartDate.HasValue)
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

        public async Task<List<CalendarEventDto>> GetAllUserEventsAsync(string userId)
        {
            var events = await _context.CalendarEvents
                .Where(e => e.ApplicationUserId == userId)
                .ToListAsync();

            return events.Select(e => new CalendarEventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                IsAllDay = e.IsAllDay,
                RecurrenceRule = e.RecurrenceRule
            }).ToList();
        }


        // Update an existing event
        public async Task<CalendarEventDto> UpdateEvent(int id, CalendarEventDto updatedEvent)
        {
            var eventToUpdate = await _context.CalendarEvents.FindAsync(id);
            if (eventToUpdate == null) return null;

            if (eventToUpdate.ApplicationUserId != updatedEvent.ApplicationUserId)
            {
                return null;
            }

            // Update properties
            eventToUpdate.Title = updatedEvent.Title;
            eventToUpdate.Description = updatedEvent.Description;
            eventToUpdate.StartDate = updatedEvent.StartDate;
            eventToUpdate.EndDate = updatedEvent.EndDate;

            await _context.SaveChangesAsync();

            // Map the updated entity to a DTO
            return new CalendarEventDto
            {
                Id = eventToUpdate.Id,
                Title = eventToUpdate.Title,
                Description = eventToUpdate.Description,
                StartDate = eventToUpdate.StartDate,
                EndDate = eventToUpdate.EndDate,
                ApplicationUserId = eventToUpdate.ApplicationUserId
            };
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
using BjjTrainer_API.Models.Calendars;
using BjjTrainer_API.Services_API.Calendars;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BjjTrainer_API.Controllers.Calendar
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly CalendarService _calendarService;

        public CalendarController(CalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventById(int eventId)
        {
            try
            {
                var calendarEvent = await _calendarService.GetEventByIdAsync(eventId);

                if (calendarEvent == null)
                {
                    return NotFound(new { Message = $"Event with ID {eventId} not found." });
                }

                return Ok(calendarEvent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching event {eventId}: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while fetching the event." });
            }
        }


        // Get all events for the logged-in user
        [HttpGet("user/{userid}")]

        public async Task<IActionResult> GetUserEvents()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                var events = await _calendarService.GetEventsForUserAsync(userId);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("events/create")]
        public async Task<IActionResult> CreateEvent([FromBody] CalendarEvent model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                Console.WriteLine($"Event Received: {model.Title}, StartTime: {model.StartTime}, EndTime: {model.EndTime}, SchoolId: {model.SchoolId}");

                // Pass to service
                var calendarEvent = await _calendarService.CreateEventAsync(userId, model);
                return Ok(new { EventId = calendarEvent.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpGet("user/schoolId")]
        public async Task<IActionResult> GetUserSchoolId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                var schoolId = await _calendarService.GetUserSchoolIdAsync(userId);
                if (schoolId == null)
                    return NotFound(new { Message = "SchoolId not found for user." });

                return Ok(new { SchoolId = schoolId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        // Update event (for coach or student)
        [HttpPut("events/{eventId}")]
        public async Task<IActionResult> UpdateEvent(int eventId, [FromBody] CalendarEvent model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                await _calendarService.UpdateEventAsync(eventId, model, userId);
                return Ok("Event updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpDelete("events/{eventId}")]

        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                await _calendarService.DeleteEventAsync(eventId, userId);
                return Ok("Event deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Check-in to event (after joining)
        [HttpPost("events/{eventId}/checkin")]
        public async Task<IActionResult> CheckInEvent(int eventId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var calendarEvent = await _calendarService.GetEventByIdAsync(eventId);

            if (calendarEvent == null)
                return NotFound("Event not found.");

            // Check if the EndDate exists
            if (calendarEvent.EndDate == null)
                return BadRequest("Event end date is missing.");

            var now = DateTime.UtcNow.Date;  // Compare dates only, ignore time
            var eventEndDate = calendarEvent.EndDate.Value.Date;

            // Check if the event has ended
            //if (now < eventEndDate)
            //    return BadRequest("Check-in only allowed after the event date.");

            // Perform check-in
            await _calendarService.CheckInEventAsync(eventId, userId);
            return Ok("Checked in successfully.");
        }
    }
}
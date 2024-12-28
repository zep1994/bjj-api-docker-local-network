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

        // Get all events for the logged-in user
        [HttpGet("events")]

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

        [HttpPost("coach/events/create")]

        public async Task<IActionResult> CreateCoachEvent([FromBody] CalendarEvent model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                var calendarEvent = await _calendarService.CreateCoachEventAsync(userId, model);
                return Ok(new { EventId = calendarEvent.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("student/events/create")]

        public async Task<IActionResult> CreateStudentEvent([FromBody] CalendarEvent model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                var calendarEvent = await _calendarService.CreateStudentEventAsync(userId, model);
                return Ok(new { EventId = calendarEvent.Id });
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
            try
            {
                await _calendarService.UpdateEventAsync(eventId, model);
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

            try
            {
                await _calendarService.CheckInEventAsync(eventId, userId);
                return Ok("Checked in successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        //[HttpGet("user/{userId}/all")]
        //public async Task<IActionResult> GetAllUserEvents(string userId)
        //{
        //    try
        //    {
        //        var events = await _calendarService.GetAllUserEventsAsync(userId);
        //        return Ok(events);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error retrieving events: {ex.Message}");
        //    }
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetEventById(int id)
        //{
        //    var eventDto = await _calendarService.GetEventByIdAsync(id);
        //    if (eventDto == null)
        //        return NotFound();

        //    return Ok(eventDto);
        //}

        //[HttpGet("user/{userId}")]
        //public async Task<IActionResult> GetUserEventsAsync(string userId)
        //{
        //    try
        //    {
        //        var events = await _calendarService.GetUserEventsAsync(userId);
        //        return Ok(events);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}
    }
}
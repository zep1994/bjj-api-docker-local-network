using BjjTrainer_API.Services_API.Coaches;
using Microsoft.AspNetCore.Mvc;

namespace BjjTrainer_API.Controllers.Coaches
{
    [Route("api/coach")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly CoachService _coachService;

        public CoachController(CoachService coachService)
        {
            _coachService = coachService;
        }

        [HttpGet("events/{schoolId}")]
        public async Task<IActionResult> GetPastEvents(int schoolId)
        {
            var events = await _coachService.GetPastEventsForSchool(schoolId);
            if (events == null) return NotFound("No past events found.");
            return Ok(events);
        }

        [HttpGet("event-checkins/{eventId}")]
        public async Task<IActionResult> GetEventCheckIns(int eventId)
        {
            var checkIns = await _coachService.GetCheckInsForEvent(eventId);
            if (checkIns == null) return NotFound("No check-ins found for this event.");
            return Ok(checkIns);
        }
    }
}

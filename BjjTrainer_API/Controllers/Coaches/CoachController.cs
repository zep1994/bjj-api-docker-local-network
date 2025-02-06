using BjjTrainer_API.Models.DTO.Calendars;
using BjjTrainer_API.Services_API.Coaches;
using BjjTrainer_API.Services_API.Trainings;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BjjTrainer_API.Controllers.Coaches
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly CoachService _coachService;
        private readonly TrainingService _trainingService;

        public CoachController(CoachService coachService, TrainingService trainingService)
        {
            _coachService = coachService;
            _trainingService = trainingService;
        }

        [HttpGet("events/full/{schoolId}")]
        public async Task<ActionResult<List<PastEventDetailsDto>>> GetPastEventsWithLogs(int schoolId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User is not authenticated.");

            try
            {
                var events = await _coachService.GetPastEventsWithLogs(userId, schoolId);
                if (!events.Any())
                    return NotFound("No past events found.");

                return Ok(events);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}

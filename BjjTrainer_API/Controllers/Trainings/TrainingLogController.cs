using BjjTrainer_API.Models.DTO;
using BjjTrainer_API.Models.Trainings;
using BjjTrainer_API.Services_API.Calendars;
using BjjTrainer_API.Services_API.Trainings;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BjjTrainer_API.Controllers.Training
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingLogController : ControllerBase
    {
        private readonly TrainingService _trainingService;
        private readonly CalendarService _calendarService;

        public TrainingLogController(TrainingService trainingService, CalendarService calendarService)
        {
            _trainingService = trainingService;
            _calendarService = calendarService;
        }

        // ******************************** GET LOG ************************************************
        [HttpGet("list/{userId}")]
        public async Task<IActionResult> GetTrainingLogs(string userId)
        {
            try
            {
                var logs = await _trainingService.GetTrainingLogsAsync(userId);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{logId}")]
        public async Task<IActionResult> GetTrainingLogById(int logId)
        {
            try
            {
                var log = await _trainingService.GetTrainingLogByIdAsync(logId);
                if (log == null)
                    return NotFound(new { message = "Training log not found." });

                return Ok(log);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ******************************** UPDATE ************************************************
        [HttpPut("{logId}")]
        public async Task<IActionResult> UpdateTrainingLog(int logId, [FromBody] CreateTrainingLogDto dto)
        {
            if (dto == null || !dto.MoveIds.Any())
                return BadRequest("Training log must include valid moves.");

            try
            {
                await _trainingService.UpdateTrainingLogAsync(logId, dto);
                return Ok(new { message = "Training log updated successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ******************************** CREATE LOG ************************************************
        [HttpPost("log")]
        public async Task<IActionResult> CreateTrainingLog([FromBody] CreateTrainingLogDto dto)
        {
            if (dto == null || !dto.MoveIds.Any())
                return BadRequest("Training log must include at least one move.");

            var trainingLog = new TrainingLog
            {
                Title = dto.Title,
                ApplicationUserId = dto.ApplicationUserId,
                Date = dto.Date,
                StartTime = dto.StartTime,
                TrainingTime = dto.TrainingTime,
                RoundsRolled = dto.RoundsRolled,
                Submissions = dto.Submissions,
                Taps = dto.Taps,
                Notes = dto.Notes,
                SelfAssessment = dto.SelfAssessment
            };

            try
            {
                await _trainingService.AddTrainingLogAsync(dto.ApplicationUserId, trainingLog, dto.MoveIds);
                return Ok(new { message = $"Training log created successfully! {trainingLog.TrainingLogMoves}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ******************************** Sharing a Student Log  ************************************************
        [HttpPost("traininglog/{logId}/share")]
        public async Task<IActionResult> ShareTrainingLog(int logId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await _trainingService.ToggleTrainingLogSharingAsync(logId, userId);
                return Ok(new
                {
                    message = "Training log sharing status updated."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // ******************************** PREFILLED LOG  ************************************************
        [HttpGet("events/{eventId}/traininglog")]
        public async Task<IActionResult> GetPreFilledTrainingLog(int eventId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var calendarEvent = await _calendarService.GetEventByIdAsync(eventId);

            if (calendarEvent == null)
                return NotFound("Event not found.");

            var trainingLogDto = new CreateTrainingLogDto
            {
                ApplicationUserId = userId,
                Date = calendarEvent.StartDate ?? DateTime.UtcNow,
                TrainingTime = 0,  // Set as 0 for now until filled in
                RoundsRolled = 0,
                Notes = $"Training log for: {calendarEvent.Title}",  // Use event title dynamically
                SelfAssessment = ""
            };

            return Ok(trainingLogDto);
        }

        [HttpDelete("{logId}")]
        public async Task<IActionResult> DeleteTrainingLog(int logId)
        {
            try
            {
                await _trainingService.DeleteTrainingLogAsync(logId);
                return Ok(new { message = "Training log deleted successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}

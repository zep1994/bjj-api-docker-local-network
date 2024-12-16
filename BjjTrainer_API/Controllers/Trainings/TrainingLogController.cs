using BjjTrainer_API.Models.DTO;
using BjjTrainer_API.Models.Trainings;
using BjjTrainer_API.Services_API.Trainings;
using Microsoft.AspNetCore.Mvc;

namespace BjjTrainer_API.Controllers.Training
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingLogController : ControllerBase
    {
        private readonly TrainingService _trainingService;

        public TrainingLogController(TrainingService trainingService)
        {
            _trainingService = trainingService;
        }

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

        [HttpPost("log")]
        public async Task<IActionResult> CreateTrainingLog([FromBody] CreateTrainingLogDto dto)
        {
            if (dto == null || !dto.MoveIds.Any())
                return BadRequest("Training log must include at least one move.");

            var trainingLog = new TrainingLog
            {
                ApplicationUserId = dto.ApplicationUserId,
                Date = dto.Date,
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

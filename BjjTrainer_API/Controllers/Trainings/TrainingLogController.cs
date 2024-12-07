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
                return Ok(new { message = "Training log created successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}

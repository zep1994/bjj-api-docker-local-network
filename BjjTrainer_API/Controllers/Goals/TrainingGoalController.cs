using BjjTrainer_API.Models.DTO;
using BjjTrainer_API.Services_API.Goals;
using Microsoft.AspNetCore.Mvc;

namespace BjjTrainer_API.Controllers.Goals
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingGoalController : ControllerBase
    {
        private readonly TrainingGoalService _goalService;

        public TrainingGoalController(TrainingGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGoal([FromBody] CreateTrainingGoalDto dto)
        {
            if (dto == null || !dto.MoveIds.Any())
                return BadRequest("A training goal must include at least one move.");

            try
            {
                await _goalService.AddTrainingGoalAsync(dto);
                return Ok(new { message = "Training goal created successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetGoals(string userId)
        {
            try
            {
                var goals = await _goalService.GetGoalsByUserAsync(userId);
                return Ok(goals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
using BjjTrainer_API.Models.DTO;
using BjjTrainer_API.Models.Moves;
using BjjTrainer_API.Models.Users;
using BjjTrainer_API.Services_API;
using Microsoft.AspNetCore.Mvc;

namespace BjjTrainer_API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProgressController : ControllerBase
    {
        private readonly TrainingService _trainingService;

        public UserProgressController(TrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpGet("{userId}/progress")]
        public async Task<ActionResult<UserProgressDto>> GetUserProgress(string userId)
        {
            try
            {
                var progress = await _trainingService.GetUserProgressAsync(userId);
                return Ok(progress);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while fetching user progress.", error = ex.Message });
            }
        }
    }
}

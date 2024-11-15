using BjjTrainer_API.Models.Training_Sessions;
using BjjTrainer_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjjTrainer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrainingSessionsController : BaseController
    {
        private readonly TrainingSessionService _trainingSessionService;

        public TrainingSessionsController(TrainingSessionService trainingSessionService) : base()
        {
            _trainingSessionService = trainingSessionService;
        }

        // POST: api/trainingsessions
        [HttpPost]
        public async Task<ActionResult<TrainingSession>> CreateTrainingSession(TrainingSession session)
        {
            //var userId = GetCurrentUserId();  
            //session.UserId = userId;

            var createdSession = await _trainingSessionService.CreateTrainingSessionAsync(session);
            return CreatedAtAction(nameof(GetTrainingSession), new { id = createdSession.Id }, createdSession);
        }

        // GET: api/trainingsessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingSession>>> GetUserTrainingSessions()
        {
            var userId = GetCurrentUserId();
            var sessions = await _trainingSessionService.GetUserTrainingSessionsAsync(userId);
            return Ok(sessions);
        }

        // GET: api/trainingsessions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingSession>> GetTrainingSession(int id)
        {
            var session = await _trainingSessionService.GetTrainingSessionByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }

        // PUT: api/trainingsessions/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<TrainingSession>> UpdateTrainingSession(int id, TrainingSession session)
        {
            var updatedSession = await _trainingSessionService.UpdateTrainingSessionAsync(id, session);
            if (updatedSession == null)
            {
                return NotFound();
            }
            return Ok(updatedSession);
        }

        // DELETE: api/trainingsessions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingSession(int id)
        {
            var success = await _trainingSessionService.DeleteTrainingSessionAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

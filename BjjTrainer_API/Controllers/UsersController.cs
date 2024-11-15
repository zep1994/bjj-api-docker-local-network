using BjjTrainer_API.Models.Lessons;
using BjjTrainer_API.Services_API;
using Microsoft.AspNetCore.Mvc;

namespace BjjTrainer_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // POST: api/users/refresh-token
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var tokens = await _userService.RefreshTokensAsync(refreshToken);

            if (tokens == null)
                return Unauthorized(new { message = "Invalid or expired refresh token." });

            return Ok(tokens);
        }

        // Get all favorite lessons for a specific user
        [HttpGet("{userId}/favorites")]
        public async Task<ActionResult<List<Lesson>>> GetUserFavorites(string userId)
        {
            var favorites = await _userService.GetUserFavoritesAsync(userId);
            return Ok(favorites);
        }

        // Add a lesson to the user's favorites
        [HttpPost("{userId}/favorites/{lessonId}")]
        public async Task<IActionResult> AddLessonToFavorites(string userId, int lessonId)
        {
            var success = await _userService.AddLessonToFavoritesAsync(userId, lessonId);
            if (success)
            {
                return Ok("Lesson added to favorites.");
            }
            return BadRequest("Failed to add lesson to favorites.");
        }

        // Remove a lesson from the user's favorites
        [HttpDelete("{userId}/favorites/{lessonId}")]
        public async Task<IActionResult> RemoveLessonFromFavorites(string userId, int lessonId)
        {
            var success = await _userService.RemoveLessonFromFavoritesAsync(userId, lessonId);
            if (success)
            {
                return Ok("Lesson removed from favorites.");
            }
            return BadRequest("Failed to remove lesson from favorites.");
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID cannot be empty.");
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }
    }
}

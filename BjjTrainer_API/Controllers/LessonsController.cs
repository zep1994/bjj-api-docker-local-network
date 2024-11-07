using BjjTrainer_API.Models.Lessons;
using BjjTrainer_API.Services_API;
using Microsoft.AspNetCore.Mvc;

namespace BjjTrainer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly LessonService _lessonService;

        public LessonsController(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        // GET: api/lessons
        [HttpGet]
        public async Task<ActionResult<List<Lesson>>> GetAllLessons()
        {
            try
            {
                var lessons = await _lessonService.GetAllLessonsAsync();
                return Ok(lessons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/lessons/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Lesson>>> GetLessonsByUser(string userId)
        {
            try
            {
                var lessons = await _lessonService.GetLessonsByUserAsync(userId);
                return Ok(lessons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/lessons/user/{userId}/add/{lessonId}
        [HttpPost("user/{userId}/add/{lessonId}")]
        public async Task<ActionResult> AddLessonToUser(string userId, int lessonId)
        {
            try
            {
                await _lessonService.AddLessonToUserAsync(userId, lessonId);
                return Ok($"Lesson {lessonId} added to user {userId}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateLesson([FromBody] Lesson lesson)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    var createdLesson = await _lessonService.CreateLessonAsync(lesson);
        //    return CreatedAtAction(nameof(GetLessonById), new { id = createdLesson.Id }, createdLesson);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id, Lesson lesson)
        {
            if (id != lesson.Id) return BadRequest();

            var result = await _lessonService.UpdateLessonAsync(lesson);
            if (!result) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var result = await _lessonService.DeleteLessonAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}

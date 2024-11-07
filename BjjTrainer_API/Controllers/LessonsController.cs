using BjjTrainer_API.Models.Lessons;
using BjjTrainer_API.Services_API;
using Microsoft.AspNetCore.Mvc;

namespace BjjTrainer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLessons()
        {
            var lessons = await _lessonService.GetAllLessonsAsync();
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonById(int id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null) return NotFound();
            return Ok(lesson);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson([FromBody] Lesson lesson)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdLesson = await _lessonService.CreateLessonAsync(lesson);
            return CreatedAtAction(nameof(GetLessonById), new { id = createdLesson.Id }, createdLesson);
        }

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

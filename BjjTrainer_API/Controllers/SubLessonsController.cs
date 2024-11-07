using BjjTrainer_API.Models.Lessons;
using BjjTrainer_API.Services_API;
using Microsoft.AspNetCore.Mvc;

namespace BjjTrainer_API.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class SubLessonsController : ControllerBase
    {
        private readonly ISubLessonService _subLessonService;

        public SubLessonsController(ISubLessonService subLessonService)
        {
            _subLessonService = subLessonService;
        }

        [HttpGet("sections/{lessonSectionId}")]
        public async Task<ActionResult<List<SubLesson>>> GetSubLessonsBySection(int lessonSectionId)
        {
            var subLessons = await _subLessonService.GetSubLessonsBySectionAsync(lessonSectionId);

            if (subLessons == null || subLessons.Count == 0)
            {
                return NotFound("No sublessons found for the provided section.");
            }

            return Ok(subLessons);
        }


        // Get a specific SubLesson by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<SubLesson>> GetSubLesson(int id)
        {
            var subLesson = await _subLessonService.GetSubLessonByIdAsync(id);
            if (subLesson == null) return NotFound();
            return Ok(subLesson);
        }

        // Create a new SubLesson
        [HttpPost]
        public async Task<ActionResult<SubLesson>> CreateSubLesson([FromBody] SubLesson subLesson)
        {
            if (subLesson == null)
            {
                return BadRequest("SubLesson data is required.");
            }

            var createdSubLesson = await _subLessonService.CreateSubLessonAsync(subLesson);
            return CreatedAtAction(nameof(GetSubLesson), new { id = createdSubLesson.Id }, createdSubLesson);
        }

        // Update an existing SubLesson
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubLesson(int id, SubLesson subLesson)
        {
            if (id != subLesson.Id) return BadRequest();
            await _subLessonService.UpdateSubLessonAsync(subLesson);
            return NoContent();
        }

        // Delete a SubLesson
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubLesson(int id)
        {
            var deleted = await _subLessonService.DeleteSubLessonAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

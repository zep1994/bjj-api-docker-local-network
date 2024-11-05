using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Lessons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LessonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<List<Lesson>>> GetAllLessons()
        {
            try
            {
                var lessons = await _context.Lessons.ToListAsync();
                if (lessons == null || lessons.Count == 0)
                {
                    return NotFound("No lessons found.");
                }
                return Ok(lessons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: api/Lessons/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(int id)
        {
            try
            {
                var lesson = await _context.Lessons.FindAsync(id);

                if (lesson == null)
                {
                    return NotFound("Lesson not found.");
                }

                return Ok(lesson);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving the lesson.");
            }
        }

        // POST: api/Lessons
        [HttpPost]
        public async Task<ActionResult<Lesson>> CreateLesson(Lesson lesson)
        {
            try
            {
                _context.Lessons.Add(lesson);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetLesson), new { id = lesson.Id }, lesson);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "An error occurred while saving the lesson. Please try again.");
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // PUT: api/Lessons/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id, Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return BadRequest("Lesson ID mismatch.");
            }

            _context.Entry(lesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Lesson updated successfully.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(id))
                {
                    return NotFound("Lesson not found.");
                }
                return StatusCode(500, "A concurrency error occurred while updating the lesson.");
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the lesson.");
            }
        }

        // DELETE: api/Lessons/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            try
            {
                var lesson = await _context.Lessons.FindAsync(id);
                if (lesson == null)
                {
                    return NotFound("Lesson not found.");
                }

                _context.Lessons.Remove(lesson);
                await _context.SaveChangesAsync();
                return Ok("Lesson deleted successfully.");
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the lesson.");
            }
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }
    }
}

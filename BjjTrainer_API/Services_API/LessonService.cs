using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Lessons;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Services_API
{
    public class LessonService
    {
        private readonly ApplicationDbContext _context;

        public LessonService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to get all lessons
        public async Task<List<Lesson>> GetAllLessonsAsync()
        {
            return await _context.Lessons.ToListAsync();
        }


        // Method to get lessons by user (many-to-many relationship)
        public async Task<List<Lesson>> GetLessonsByUserAsync(string userId)
        {
            var user = await _context.ApplicationUsers
                                      .Include(u => u.Lessons)
                                      .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user.Lessons.ToList();
        }

        // Method to add a lesson to a user's list
        public async Task AddLessonToUserAsync(string userId, int lessonId)
        {
            var user = await _context.ApplicationUsers
                                      .Include(u => u.Lessons)
                                      .FirstOrDefaultAsync(u => u.Id == userId);

            var lesson = await _context.Lessons
                                       .FirstOrDefaultAsync(l => l.Id == lessonId);

            if (user == null || lesson == null)
            {
                throw new Exception("User or Lesson not found");
            }

            // Add the lesson to the user's list (many-to-many relationship)
            if (!user.Lessons.Contains(lesson))
            {
                user.Lessons.Add(lesson);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Lesson> CreateLessonAsync(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<bool> UpdateLessonAsync(Lesson lesson)
        {
            // Retrieve the existing lesson from the database
            var existingLesson = await _context.Lessons.FindAsync(lesson.Id);
            if (existingLesson == null)
            {
                return false; // Lesson not found
            }

            // Update the properties of the existing lesson
            existingLesson.Title = lesson.Title;
            existingLesson.Description = lesson.Description;
            existingLesson.Belt = lesson.Belt;
            existingLesson.Category = lesson.Category;
            existingLesson.Difficulty = lesson.Difficulty;

            // Save changes to the database
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteLessonAsync(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null) return false;

            _context.Lessons.Remove(lesson);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
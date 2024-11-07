using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Lessons;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Services_API
{
    public class LessonService : ILessonService
    {
        private readonly ApplicationDbContext _context;

        public LessonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lesson>> GetAllLessonsAsync()
        {
            return await _context.Lessons.ToListAsync();
        }

        public async Task<Lesson> GetLessonByIdAsync(int id)
        {
            return await _context.Lessons.FindAsync(id);
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
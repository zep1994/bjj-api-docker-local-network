using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Lessons;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Services_API
{
    public class SubLessonService(ApplicationDbContext context) : ISubLessonService
    {
        private readonly ApplicationDbContext _context = context;

        // Get all SubLessons for a specific LessonSection
        public async Task<List<SubLesson>> GetSubLessonsBySectionAsync(int lessonSectionId)
        {
            var subLessons = await _context.SubLessons
                .Where(sl => sl.LessonSectionId == lessonSectionId)
                .ToListAsync();

            return subLessons;  
        }


        // Get a specific SubLesson by ID
        public async Task<SubLesson> GetSubLessonByIdAsync(int id)
        {
            return await _context.SubLessons.FindAsync(id);
        }

        // Create a new SubLesson
        public async Task<SubLesson> CreateSubLessonAsync(SubLesson subLesson)
        {
            _context.SubLessons.Add(subLesson);
            await _context.SaveChangesAsync();
            return subLesson;
        }

        // Update an existing SubLesson
        public async Task<SubLesson> UpdateSubLessonAsync(SubLesson subLesson)
        {
            _context.Entry(subLesson).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return subLesson;
        }

        // Delete a SubLesson
        public async Task<bool> DeleteSubLessonAsync(int id)
        {
            var subLesson = await _context.SubLessons.FindAsync(id);
            if (subLesson == null) return false;

            _context.SubLessons.Remove(subLesson);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

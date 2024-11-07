using BjjTrainer_API.Data;
using BjjTrainer_API.Models.DTO;
using BjjTrainer_API.Models.Lessons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Services_API
{
    public class SubLessonService
    {
        private readonly ApplicationDbContext _context;

        // Constructor-based injection for ApplicationDbContext
        public SubLessonService(ApplicationDbContext context)
        {
            _context = context;
        }
        // Get all SubLessons for a specific LessonSection
        public async Task<List<SubLesson>> GetSubLessonsBySectionAsync(int lessonSectionId)
        {
            try
            {
                var subLessons = await _context.SubLessons
                    .Where(sl => sl.LessonSectionId == lessonSectionId)
                    .ToListAsync();

                return subLessons;
            }
            catch (Exception ex)
            {
                // Handle and log error here (you may also return a custom error)
                throw new InvalidOperationException("Error fetching sub-lessons for the given section.", ex);
            }
        }

        public async Task<SubLessonDetailsDto> GetSubLessonDetailsByIdAsync(int id)
        {
            try
            {
                var subLesson = await _context.SubLessons.FindAsync(id);
                if (subLesson == null)
                {
                    return null; // If no sub-lesson found, return null
                }

                return new SubLessonDetailsDto
                {
                    Id = subLesson.Id,
                    Title = subLesson.Title,
                    Content = subLesson.Content,
                    Notes = subLesson.Notes
                };
            }
            catch (Exception ex)
            {
                // Handle and log error here
                throw new InvalidOperationException("Error fetching sub-lesson details.", ex);
            }
        }

        // Get a specific SubLesson by ID
        public async Task<SubLesson> GetSubLessonByIdAsync(int id)
        {
            var subLesson = await _context.SubLessons
                                 .FirstOrDefaultAsync(sl => sl.Id == id);
            return subLesson;
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

        public Task<ActionResult<SubLessonDetailsDto>> GetSubLessonDetails(int id)
        {
            throw new NotImplementedException();
        }
    }
}
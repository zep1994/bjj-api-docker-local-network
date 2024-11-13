using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Lessons;
using BjjTrainer_API.Models.User;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Services_API
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Fetch user by ID
        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _context.ApplicationUsers
                .Include(u => u.Lessons)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        // Get all favorite lessons for a user
        public async Task<List<Lesson>> GetUserFavoritesAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            return user?.Lessons.ToList() ?? new List<Lesson>();
        }

        // Add lesson to user's favorites
        public async Task<bool> AddLessonToFavoritesAsync(string userId, int lessonId)
        {
            var user = await GetUserByIdAsync(userId);
            var lesson = await _context.Lessons.FindAsync(lessonId);

            if (user != null && lesson != null && !user.Lessons.Contains(lesson))
            {
                user.Lessons.Add(lesson);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Remove lesson from user's favorites
        public async Task<bool> RemoveLessonFromFavoritesAsync(string userId, int lessonId)
        {
            var user = await GetUserByIdAsync(userId);
            var lesson = await _context.Lessons.FindAsync(lessonId);

            if (user != null && lesson != null && user.Lessons.Contains(lesson))
            {
                user.Lessons.Remove(lesson);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

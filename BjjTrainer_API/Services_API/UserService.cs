using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Lessons;
using BjjTrainer_API.Models.User;
using BjjTrainer_API.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BjjTrainer_API.Services_API
{
    public class UserService(ApplicationDbContext context, IConfiguration configuration)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IConfiguration _configuration = configuration;

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

        public async Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(string userId)
        {
            var accessToken = GenerateAccessToken(userId);
            var refreshToken = await CreateRefreshTokenAsync(userId);

            return (accessToken, refreshToken.Token);
        }

        private string GenerateAccessToken(string userId)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(30), // Access token validity
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<RefreshToken> CreateRefreshTokenAsync(string userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<(string AccessToken, string RefreshToken)?> RefreshTokensAsync(string refreshToken)
        {
            var existingToken = await _context.RefreshTokens
                .SingleOrDefaultAsync(rt => rt.Token == refreshToken && rt.IsActive);

            if (existingToken == null || existingToken.IsExpired) return null;

            // Revoke the old refresh token and generate new tokens
            existingToken.Revoked = DateTime.UtcNow;
            var tokens = await GenerateTokensAsync(existingToken.UserId);

            await _context.SaveChangesAsync();
            return tokens;
        }
    }
}

﻿using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Lessons;
using BjjTrainer_API.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BjjTrainer_API.Services_API.Users
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
                .Include(u => u.Moves)
                .Include(u => u.TrainingLogs)
                .Include(u => u.TrainingGoals)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        // Get all favorite lessons for a user
        public async Task<List<Lesson>> GetUserFavoritesAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            return user?.Lessons.ToList() ?? [];
        }

        // GET SCHOOL
        public async Task<List<ApplicationUser>> GetUsersBySchoolAsync(int schoolId)
        {
            return await _context.ApplicationUsers
                .Where(u => u.SchoolId == schoolId)
                .ToListAsync();
        }

        public async Task<string> AddUsersToSchoolAsync(string coachId, int schoolId, List<string> emails)
        {
            var coach = await _context.ApplicationUsers.Include(u => u.School)
                .FirstOrDefaultAsync(u => u.Id == coachId && u.IsCoach);

            if (coach == null || coach.SchoolId != schoolId)
            {
                return "You are not authorized to add users to this school.";
            }

            var school = await _context.Schools.FindAsync(schoolId);
            if (school == null)
            {
                return "School not found.";
            }

            var results = new List<string>();

            foreach (var email in emails)
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    results.Add($"User with email {email} not found.");
                    continue;
                }

                if (user.SchoolId == schoolId)
                {
                    results.Add($"User {email} is already a member of this school.");
                    continue;
                }

                if (user.SchoolId != null)
                {
                    var existingSchool = await _context.Schools.FindAsync(user.SchoolId);
                    results.Add($"User {email} is already a member of \"{existingSchool?.Name}\". Add them anyway?");
                    continue;
                }

                user.SchoolId = schoolId;
                _context.ApplicationUsers.Update(user);
                results.Add($"User {email} has been added to the school.");
            }

            await _context.SaveChangesAsync();
            return string.Join("\n", results);
        }


        public async Task<bool> UpdateUserSchoolAsync(string userId, int schoolId)
        {
            var user = await _context.ApplicationUsers.FindAsync(userId);
            if (user == null) return false;

            var school = await _context.Schools.FindAsync(schoolId);
            if (school == null) return false;

            user.SchoolId = schoolId;
            _context.ApplicationUsers.Update(user);
            await _context.SaveChangesAsync();
            return true;
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

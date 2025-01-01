using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Services_API.Users
{
    public class SchoolService
    {
        private readonly ApplicationDbContext _context;

        public SchoolService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateSchoolAndAddCoachAsync(string coachId, string schoolName, string schoolAddress, string schoolPhone)
        {
            var coach = await _context.ApplicationUsers.Include(u => u.School)
                .FirstOrDefaultAsync(u => u.Id == coachId && u.Role == UserRole.Coach);

            if (coach == null)
            {
                return "User is not authorized to create a school.";
            }

            if (coach.SchoolId != null)
            {
                var existingSchool = await _context.Schools.FindAsync(coach.SchoolId);
                return $"You are already a member of \"{existingSchool?.Name}\". Do you want to change schools?";
            }

            var school = new School
            {
                Name = schoolName,
                Address = schoolAddress,
                Phone = schoolPhone
            };

            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            // Assign the newly created school to the coach
            coach.SchoolId = school.Id;
            _context.ApplicationUsers.Update(coach);
            await _context.SaveChangesAsync();

            return $"School \"{schoolName}\" created and you have been added as a coach.";
        }

        public async Task<List<School>> GetAllSchoolsAsync()
        {
            return await _context.Schools.ToListAsync();
        }
    }

}

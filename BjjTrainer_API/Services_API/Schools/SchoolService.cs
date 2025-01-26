using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Schools;
using BjjTrainer_API.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Services_API.Schools
{
    public class SchoolService
    {
        private readonly ApplicationDbContext _context;

        public SchoolService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<School?> GetSchoolByIdAsync(string coachId, int schoolId)
        {
            var coach = await _context.ApplicationUsers.Include(u => u.School)
                .FirstOrDefaultAsync(u => u.Id == coachId && u.Role == UserRole.Coach);

            if (coach == null || coach.SchoolId != schoolId)
            {
                return null; // Coach is not authorized or the school does not exist
            }

            return await _context.Schools.FirstOrDefaultAsync(s => s.Id == schoolId);
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

        public async Task<bool> UpdateSchoolAsync(string coachId, int schoolId, string name, string address, string phone)
        {
            var coach = await _context.ApplicationUsers.Include(u => u.School)
                .FirstOrDefaultAsync(u => u.Id == coachId && u.Role == UserRole.Coach);

            if (coach == null || coach.SchoolId != schoolId)
            {
                return false; // Ensure the coach is authorized to manage this school
            }

            var school = await _context.Schools.FindAsync(schoolId);
            if (school == null)
            {
                return false; // School does not exist
            }

            school.Name = name;
            school.Address = address;
            school.Phone = phone;

            _context.Schools.Update(school);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSchoolAsync(string coachId, int schoolId)
        {
            var coach = await _context.ApplicationUsers.Include(u => u.School)
                .FirstOrDefaultAsync(u => u.Id == coachId && u.Role == UserRole.Coach);

            if (coach == null || coach.SchoolId != schoolId)
            {
                return false; // Ensure the coach is authorized to manage this school
            }

            var school = await _context.Schools.Include(s => s.Users).FirstOrDefaultAsync(s => s.Id == schoolId);
            if (school == null || school.Users.Any())
            {
                return false; // School does not exist or has associated users
            }

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
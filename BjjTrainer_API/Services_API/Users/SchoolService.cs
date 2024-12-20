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

        public async Task<School> CreateSchoolAsync(string name, string address, string phone)
        {
            var school = new School { Name = name, Address = address, Phone = phone };
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();
            return school;
        }

        public async Task<List<School>> GetAllSchoolsAsync()
        {
            return await _context.Schools.ToListAsync();
        }
    }

}

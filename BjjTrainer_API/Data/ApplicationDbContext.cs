using BjjTrainer_API.Models.Lessons;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Lesson> Lessons { get; set; }

    }
}

using BjjTrainer_API.Models.Lessons;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonSection> LessonSections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Model configurations can be added here
            base.OnModelCreating(modelBuilder);

            // Configure the Lesson entity
            modelBuilder.Entity<Lesson>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Lesson>()
                .Property(l => l.Title)
                .IsRequired();

            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.LessonSections)
                .WithOne() // No navigation property in LessonSection
                .HasForeignKey(ls => ls.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the LessonSection entity
            modelBuilder.Entity<LessonSection>()
                .HasKey(ls => ls.Id);

            modelBuilder.Entity<LessonSection>()
                .Property(ls => ls.Title)
                .IsRequired();

            modelBuilder.Entity<LessonSection>()
                .Property(ls => ls.Description)
                .IsRequired();

            modelBuilder.Entity<LessonSection>()
                .Property(ls => ls.Difficulty)
                .IsRequired();
        }

    }
}

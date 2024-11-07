using BjjTrainer_API.Models.Lessons;
using BjjTrainer_API.Models.User;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonSection> LessonSections { get; set; }
        public DbSet<SubLesson> SubLessons { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Lesson entity
            modelBuilder.Entity<Lesson>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Lesson>()
                .Property(l => l.Title)
                .IsRequired();

            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.LessonSections)
                .WithOne(s => s.Lesson)
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

            modelBuilder.Entity<LessonSection>()
                .HasMany(ls => ls.SubLessons)
                .WithOne(sl => sl.LessonSection)
                .HasForeignKey(sl => sl.LessonSectionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the many-to-many relationship between ApplicationUser and Lesson
            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.ApplicationUsers)
                .WithMany(u => u.Lessons)
                .UsingEntity<Dictionary<string, object>>(
                    "ApplicationUserLessonJoin", // Join table name
                    j => j.HasOne<ApplicationUser>()
                          .WithMany()
                          .HasForeignKey("ApplicationUserId") // Foreign key to ApplicationUser
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Lesson>()
                          .WithMany()
                          .HasForeignKey("LessonId") // Foreign key to Lesson
                          .OnDelete(DeleteBehavior.Cascade)
                );

            modelBuilder.Entity<Lesson>()
                .ToTable("Lessons");
            modelBuilder.Entity<SubLesson>()
                .ToTable("SubLessons");
        }
    }
}

using BjjTrainer_API.Models.Joins;
using BjjTrainer_API.Models.Lessons;
using BjjTrainer_API.Models.Moves;
using BjjTrainer_API.Models.Users;
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
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<SubLessonMove> SubLessonMoves { get; set; }
        public DbSet<TrainingLog> TrainingLogs { get; set; }
        public DbSet<TrainingLogMove> TrainingLogMoves { get; set; }


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

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Lessons)
                .WithMany(l => l.ApplicationUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "ApplicationUserLessonJoin",
                    j => j
                        .HasOne<Lesson>()
                        .WithMany()
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<ApplicationUser>()
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                );


            modelBuilder.Entity<Lesson>()
                .ToTable("Lessons");
            modelBuilder.Entity<SubLesson>()
                .ToTable("SubLessons");

            // Configure many-to-many relationship between SubLesson and Move
            modelBuilder.Entity<SubLessonMove>()
                .HasKey(sl => new { sl.SubLessonId, sl.MoveId });

            modelBuilder.Entity<SubLessonMove>()
                .HasOne(sl => sl.SubLesson)
                .WithMany(s => s.SubLessonMoves)
                .HasForeignKey(sl => sl.SubLessonId);

            modelBuilder.Entity<SubLessonMove>()
                .HasOne(sl => sl.Move)
                .WithMany(m => m.SubLessonMoves)
                .HasForeignKey(sl => sl.MoveId);

            // Configure TrainingLogMove join table
            modelBuilder.Entity<TrainingLogMove>()
                .HasKey(tlm => new { tlm.TrainingLogId, tlm.MoveId });

            modelBuilder.Entity<TrainingLogMove>()
                .HasOne(tlm => tlm.TrainingLog)
                .WithMany(tl => tl.TrainingLogMoves)
                .HasForeignKey(tlm => tlm.TrainingLogId);

            modelBuilder.Entity<TrainingLogMove>()
                .HasOne(tlm => tlm.Move)
                .WithMany(m => m.TrainingLogMoves)
                .HasForeignKey(tlm => tlm.MoveId);

            // TrainingLog -> ApplicationUser
            modelBuilder.Entity<TrainingLog>()
                .HasOne(t => t.ApplicationUser)
                .WithMany(u => u.TrainingLogs) // Ensure ApplicationUser has a TrainingLogs collection
                .HasForeignKey(t => t.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust deletion behavior as needed
        }
    }
}

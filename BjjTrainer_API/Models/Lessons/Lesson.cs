using BjjTrainer_API.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.Lessons
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Belt { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation property for LessonSections
        public ICollection<LessonSection>? LessonSections { get; set; } = new List<LessonSection>();

        // Many-to-many relationship with ApplicationUser
        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
    }


}

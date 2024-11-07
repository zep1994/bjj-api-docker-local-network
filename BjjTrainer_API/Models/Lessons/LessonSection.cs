using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.Lessons
{
    public class LessonSection
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Difficulty {  get; set; } = string.Empty;
        [Required]
        public int Order { get; set; } // Order within the lesson

        public Lesson? Lesson { get; set; }

        // Foreign key to the Lesson
        [ForeignKey("Lesson")]
        public int LessonId { get; set; }

        // Navigation property to represent the one-to-many relationship
        public ICollection<SubLesson> SubLessons { get; set; } = [];
  

    }
}

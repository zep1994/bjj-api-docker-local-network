using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.Lessons
{
    public class SubLesson
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(1);
        public string VideoUrl { get; set; } = String.Empty;
        public string DocumentUrl { get; set; } = String.Empty;
        public string[] Tags { get; set; }  = [];
        public string SkillLevel { get; set; } = String.Empty;
        public string Notes { get; set; } = String.Empty;

        [ForeignKey("LessonSection")]
        public int LessonSectionId { get; set; }

        // Navigation property for the related LessonSection
        public LessonSection? LessonSection { get; set; }


    }
}

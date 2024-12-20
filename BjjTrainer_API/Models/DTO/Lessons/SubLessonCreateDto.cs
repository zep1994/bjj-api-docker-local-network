namespace BjjTrainer_API.Models.DTO.Lessons
{
    public class SubLessonCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public string VideoUrl { get; set; } = string.Empty;
        public string DocumentUrl { get; set; } = string.Empty;
        public string[] Tags { get; set; } = [];
        public string SkillLevel { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int LessonSectionId { get; set; }
        public int? MoveId { get; set; }
    }
}

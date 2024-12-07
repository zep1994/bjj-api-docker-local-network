namespace BjjTrainer_API.Models.DTO
{
    public class MoveDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? SkillLevel { get; set; }
        public List<string>? Tags { get; set; } = new List<string>();
        public int TrainingLogCount { get; set; }
    }
}

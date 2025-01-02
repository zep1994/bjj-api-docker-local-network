namespace BjjTrainer.Models.Moves
{
    public class MoveDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string? SkillLevel { get; set; }
        public List<string>? Tags { get; set; }
        public int TrainingLogCount { get; set; }
        public bool IsSelected { get; set; }
    }
}

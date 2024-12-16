namespace BjjTrainer.Models.Moves
{
    public class Move
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? SkillLevel { get; set; }
        public string? Category { get; set; }
        public string? StartingPosition { get; set; }
        public string? History { get; set; }
        public string? Scenarios { get; set; }
        public bool? LegalInCompetitions { get; set; }
        public string? CounterStrategies { get; set; }
        public List<string>? Tags { get; set; } = new List<string>();
        public int TrainingLogCount { get; set; }

        public bool IsSelected { get; set; } = false;

    }
}

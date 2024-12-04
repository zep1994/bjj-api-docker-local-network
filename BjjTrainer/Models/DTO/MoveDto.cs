namespace BjjTrainer.Models.DTO
{
    public class MoveDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string? SkillLevel { get; set; }

        public string? Category { get; set; }

        public string? StartingPosition { get; set; }

        public string? History { get; set; }

        public string? Scenarios { get; set; }

        public bool? LegalInCompetitions { get; set; }

        public string? CounterStrategies { get; set; }

        public List<string>? Tags { get; set; }
    }
}

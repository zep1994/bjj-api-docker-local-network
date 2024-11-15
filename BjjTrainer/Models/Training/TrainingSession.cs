namespace BjjTrainer.Models.Training
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateOnly? Date { get; set; }
        public string? Location { get; set; }
        public string? Notes { get; set; }
        public List<string>? Tags { get; set; }
        public string? TypeOfTraining { get; set; }
        public List<string>? AreasTrained { get; set; }
        public List<string>? MovesTrained { get; set; }
        public List<string>? LessonMoves { get; set; }
        public string UserId { get; set; }
        public int? IntensityLevel { get; set; }
        public int? Rating { get; set; }
    }
}

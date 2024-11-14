namespace BjjTrainer.Models.Training
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
    }
}

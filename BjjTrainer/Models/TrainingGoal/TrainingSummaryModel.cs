namespace BjjTrainer.Models.TrainingGoal
{
    public class TrainingSummaryModel
    {
        public int TotalTrainingLogs { get; set; }
        public string TotalTrainingTime { get; set; } = string.Empty;
        public string AverageTrainingTime { get; set; } = string.Empty;
    }
}

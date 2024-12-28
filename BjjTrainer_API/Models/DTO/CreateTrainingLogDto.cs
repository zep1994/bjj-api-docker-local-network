namespace BjjTrainer_API.Models.DTO
{
    public class CreateTrainingLogDto
    {
        public string ApplicationUserId { get; set; }
        public DateTime Date { get; set; }
        public double TrainingTime { get; set; }
        public int RoundsRolled { get; set; }
        public int Submissions { get; set; }
        public int Taps { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string SelfAssessment { get; set; } = "Learning";
        public List<int> MoveIds { get; set; } = [];
    }
}

namespace BjjTrainer_API.Models.DTO.TrainingLogDTOs
{
    public class UpdateTrainingLogDto
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }  // Fix: Add ApplicationUserId
        public DateTime Date { get; set; }
        public double TrainingTime { get; set; }
        public int RoundsRolled { get; set; }
        public int Submissions { get; set; }
        public int Taps { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string SelfAssessment { get; set; } = string.Empty;
        public bool IsCoachLog { get; set; }
        public List<int> MoveIds { get; set; } = new List<int>();
    }

}

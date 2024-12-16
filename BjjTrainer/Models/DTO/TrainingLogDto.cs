using BjjTrainer.Models.Moves;

namespace BjjTrainer.Models.DTO
{
    public class TrainingLogDto
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public double TrainingTime { get; set; } = 0;
        public int RoundsRolled { get; set; } = 0;
        public int Submissions { get; set; } = 0;
        public int Taps { get; set; } = 0;
        public string? Notes { get; set; } = string.Empty;
        public string SelfAssessment { get; set; } = string.Empty;
        public List<Move> Moves { get; set; } = new List<Move>();
    }
}

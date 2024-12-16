using BjjTrainer.Models.Moves;

namespace BjjTrainer.Models.DTO
{
    public class TrainingLogDto
    {
        public int Id { get; set; }
        public string? ApplicationUserId { get; set; }
        public DateTime Date { get; set; }
        public double TrainingTime { get; set; }
        public int RoundsRolled { get; set; }
        public int Submissions { get; set; }
        public int Taps { get; set; }
        public string? Notes { get; set; }
        public string? SelfAssessment { get; set; }
        public List<Move>? Moves { get; set; }
    }
}

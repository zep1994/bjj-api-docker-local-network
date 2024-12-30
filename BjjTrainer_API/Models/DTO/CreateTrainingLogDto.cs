using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.DTO
{
    public class CreateTrainingLogDto
    {
        public string? Title { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; } // StartDate
        [Column(TypeName = "time")]
        public TimeSpan? StartTime { get; set; }
        public string ApplicationUserId { get; set; }
        public double TrainingTime { get; set; } = 0;
        public int RoundsRolled { get; set; } = 0;
        public int Submissions { get; set; } = 0;
        public int Taps { get; set; } = 0;
        public string Notes { get; set; } = string.Empty;
        public string SelfAssessment { get; set; } = string.Empty;
        public List<int> MoveIds { get; set; } = [];
    }
}

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

        public string DayColor => GetDayColor(Date.DayOfWeek);
        public string DayInitial => Date.DayOfWeek.ToString().Substring(0, 1).ToUpper();

        private string GetDayColor(DayOfWeek day)
        {
            return day switch
            {
                DayOfWeek.Monday => "#FF5733",
                DayOfWeek.Tuesday => "#33FF57",
                DayOfWeek.Wednesday => "#3357FF",
                DayOfWeek.Thursday => "#FF33A1",
                DayOfWeek.Friday => "#A133FF",
                DayOfWeek.Saturday => "#FFD700",
                DayOfWeek.Sunday => "#FF6347",
                _ => "#FFFFFF"
            };
        }
    }
}

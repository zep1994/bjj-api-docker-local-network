using BjjTrainer.Models.Move;

namespace BjjTrainer.Models.DTO
{
    public class UserProgressDto
    {
        public double TotalTrainingTime { get; set; }
        public int TotalRoundsRolled { get; set; }
        public int TotalSubmissions { get; set; }
        public int TotalTaps { get; set; }
        public List<MoveDto> MovesPerformed { get; set; } = new List<MoveDto>();
    }
}

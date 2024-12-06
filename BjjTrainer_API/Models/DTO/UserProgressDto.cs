using BjjTrainer_API.Models.Moves;

namespace BjjTrainer_API.Models.DTO
{
    public class UserProgressDto
    {
        public double TotalTrainingTime { get; set; }
        public int TotalRoundsRolled { get; set; }
        public int TotalSubmissions { get; set; }
        public int TotalTaps { get; set; }
        public List<MoveDto>? MovesPerformed { get; set; }
    }
}

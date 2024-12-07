using BjjTrainer.Models.Move;

namespace BjjTrainer.Models.TrainingGoal
{
    public class TrainingGoal
    {
        public int Id { get; set; }
        public DateTime GoalDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public List<MoveDto> Moves { get; set; } = new List<MoveDto>();
    }
}

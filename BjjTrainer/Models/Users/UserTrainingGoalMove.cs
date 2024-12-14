using BjjTrainer.Models.Move;

namespace BjjTrainer.Models.Users
{
    public class UserTrainingGoalMove
    {
        public int TrainingGoalId { get; set; }
        public int MoveId { get; set; }
        public MoveDto Move { get; set; }
    }
}

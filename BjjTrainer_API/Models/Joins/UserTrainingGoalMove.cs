using BjjTrainer_API.Models.Goals;
using BjjTrainer_API.Models.Moves;

namespace BjjTrainer_API.Models.Joins
{
    public class UserTrainingGoalMove
    {
        public int TrainingGoalId { get; set; }
        public TrainingGoal TrainingGoal { get; set; } = null!;

        public int MoveId { get; set; }
        public Move Move { get; set; } = null!;
    }
}

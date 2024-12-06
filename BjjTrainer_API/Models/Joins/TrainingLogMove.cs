using BjjTrainer_API.Models.Moves;
using BjjTrainer_API.Models.Users;

namespace BjjTrainer_API.Models.Joins
{
    public class TrainingLogMove
    {
        public int TrainingLogId { get; set; }
        public TrainingLog TrainingLog { get; set; }

        public int MoveId { get; set; }
        public Move Move { get; set; }

        // Optional: Allow tracking self-assessment per move in a session
        public string SelfAssessment { get; set; } = "Learning";
    }
}

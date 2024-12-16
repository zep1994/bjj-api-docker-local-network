using BjjTrainer_API.Models.Moves;
using BjjTrainer_API.Models.Trainings;

namespace BjjTrainer_API.Models.Joins
{
    public class TrainingLogMove
    {
        public int TrainingLogId { get; set; }
        public TrainingLog TrainingLog { get; set; }

        public int MoveId { get; set; }
        public Move Move { get; set; }
    }
}

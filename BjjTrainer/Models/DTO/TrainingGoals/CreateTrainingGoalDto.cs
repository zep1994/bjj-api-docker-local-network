namespace BjjTrainer.Models.DTO.TrainingGoals
{
    public class CreateTrainingGoalDto
    {
        public string ApplicationUserId { get; set; } = string.Empty; // Associated user ID
        public DateTime GoalDate { get; set; } // Date for the training goal
        public string Notes { get; set; } = string.Empty; // Notes about the training goal
        public List<int> MoveIds { get; set; } = new(); // IDs of the associated moves
    }
}

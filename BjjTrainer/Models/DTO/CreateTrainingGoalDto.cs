namespace BjjTrainer.Models.DTO
{
    public class CreateTrainingGoalDto
    {
        public string ApplicationUserId { get; set; } = string.Empty;
        public DateTime GoalDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public List<int> MoveIds { get; set; } = new List<int>();
    }
}

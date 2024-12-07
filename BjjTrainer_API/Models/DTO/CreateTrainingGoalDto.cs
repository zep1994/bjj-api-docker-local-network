using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.DTO
{
    public class CreateTrainingGoalDto
    {
        public string ApplicationUserId { get; set; }
        [Column(TypeName = "date")]
        public DateTime GoalDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public List<int> MoveIds { get; set; } = new List<int>();
    }
}

using BjjTrainer_API.Models.Joins;
using BjjTrainer_API.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.Goals
{
    public class TrainingGoal
    {
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime GoalDate { get; set; }
        public string Notes { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<UserTrainingGoalMove> UserTrainingGoalMoves { get; set; } = new List<UserTrainingGoalMove>();

        // Foreign key for the ApplicationUser
        public string ApplicationUserId { get; set; } = string.Empty;
        // Navigation property for the ApplicationUser
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}

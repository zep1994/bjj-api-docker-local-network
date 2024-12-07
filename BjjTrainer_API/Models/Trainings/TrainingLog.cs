using BjjTrainer_API.Models.Joins;
using BjjTrainer_API.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.Trainings
{
    public class TrainingLog
    {
        public int Id { get; set; }

        // Foreign key to ApplicationUser
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }  // Ensure this matches the type of the `Id` in ApplicationUser
        public ApplicationUser ApplicationUser { get; set; }

        [Column(TypeName = "date")] // Use 'date' type for PostgreSQL
        public DateTime Date { get; set; } // Change to DateOnly

        // Training metrics for this session
        public double TrainingTime { get; set; } // Time in minutes
        public int RoundsRolled { get; set; } // Number of rounds the user rolled
        public int Submissions { get; set; } // Number of submissions executed
        public int Taps { get; set; } // Number of taps received

        // User feedback or notes for the session
        public string Notes { get; set; } = string.Empty;

        // Self-assessment options (Learning, Applicable, Proficient) for techniques learned
        public string SelfAssessment { get; set; } = "Learning";
        // Relationship with Moves
        public ICollection<TrainingLogMove> TrainingLogMoves { get; set; } = new List<TrainingLogMove>();
    }
}

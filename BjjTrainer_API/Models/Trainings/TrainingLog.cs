using BjjTrainer_API.Models.Joins;
using BjjTrainer_API.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.Trainings
{
    public class TrainingLog
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        [Column(TypeName = "date")] 
        public DateTime Date { get; set; } // StartDate
        [Column(TypeName = "time")]
        public TimeSpan? StartTime { get; set; } 
        // Training metrics for this session
        public double TrainingTime { get; set; } = 0; //HH:MM
        public int RoundsRolled { get; set; } = 0; 
        public int Submissions { get; set; } = 0;
        public int Taps { get; set; } = 0;
        public string Notes { get; set; } = string.Empty;

        // Self-assessment options (Learning, Applicable, Proficient) for techniques learned
        public string SelfAssessment { get; set; } = string.Empty;
        // Relationship with Moves
        public ICollection<TrainingLogMove> TrainingLogMoves { get; set; } = [];
        // Foreign key to ApplicationUser
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

using BjjTrainer_API.Models.Lessons;
using BjjTrainer_API.Models.Moves;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.Users
{
    [Table("ApplicationUsers")]
    public class ApplicationUser : IdentityUser
    {
        // Track lessons the user has engaged with
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

        // Track the moves the user has learned or practiced
        public ICollection<Move> Moves { get; set; } = new List<Move>();

        public ICollection<TrainingLog> TrainingLogs { get; set; } = new List<TrainingLog>();

        // Track User Start Date
        public DateTime? TrainingStartDate { get; set; }

        // Add training stats for progress tracking
        public int TotalSubmissions { get; set; } = 0;
        public int TotalTaps { get; set; } = 0;
        public double TotalTrainingTime { get; set; } = 0; // in minutes
        public int TotalRoundsRolled { get; set; } = 0;

        // For belt tracking or progress
        public string Belt { get; set; } = "White";
        public int BeltStripes { get; set; } = 0;
    }
}
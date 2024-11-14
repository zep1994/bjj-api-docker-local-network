using BjjTrainer_API.Models.User;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BjjTrainer_API.Models.Training_Sessions
{
    public class TrainingSession
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "Training Session";

        public TimeSpan? Duration { get; set; }

        public DateOnly? Date { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }  

        public string? Location { get; set; } 

        public List<string>? Tags { get; set; }

        public required string UserId { get; set; } 

        public List<string> AreasTrained { get; set; } = new List<string>();

        public List<string> MovesTrained { get; set; } = new List<string>();

        public string TypeOfTraining { get; set; } = "Class";

        public List<string> LessonMoves { get; set; }  = new List<string>();  

        public int? IntensityLevel { get; set; } 

        public int? Rating { get; set; }

    }
}

using BjjTrainer.Models.Moves.BjjTrainer.Models.DTO.Moves;
using Syncfusion.Maui.DataForm;
using System.Collections.ObjectModel;

namespace BjjTrainer.Models.DTO.TrainingLog
{
    public class UpdateTrainingLogDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double TrainingTime { get; set; }
        public int RoundsRolled { get; set; }
        public int Submissions { get; set; }
        public int Taps { get; set; }
        public string? Notes { get; set; }
        public string? SelfAssessment { get; set; }
        public bool IsCoachLog { get; set; }
        public List<int> MoveIds { get; set; } = new();
        public ObservableCollection<UpdateMoveDto> Moves { get; set; } = new();
        public string? ApplicationUserId { get; set; }
    }
}

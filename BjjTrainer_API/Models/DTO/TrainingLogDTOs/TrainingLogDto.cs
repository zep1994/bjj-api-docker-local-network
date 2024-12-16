namespace BjjTrainer_API.Models.DTO.TrainingLogDTOs
{
    public class TrainingLogDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double TrainingTime { get; set; }
        public int RoundsRolled { get; set; }
        public int Submissions { get; set; }
        public int Taps { get; set; }
        public string Notes { get; set; }
        public string SelfAssessment { get; set; }
        public List<MoveDto> Moves { get; set; } = new List<MoveDto>();
    }
}

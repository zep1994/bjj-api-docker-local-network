namespace BjjTrainer.Models.DTO
{
    public class TrainingLogDto
    {
        public string ApplicationUserId { get; set; }
        public string Date { get; set; }
        public double TrainingTime { get; set; }
        public int RoundsRolled { get; set; }
        public int Submissions { get; set; }
        public int Taps { get; set; }
        public string Notes { get; set; }
        public List<int> MoveIds { get; set; }
    }
}

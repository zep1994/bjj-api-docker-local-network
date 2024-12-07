using BjjTrainer_API.Models.Moves;

namespace BjjTrainer_API.Models.DTO
{
    public class SubLessonDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public List<Move> Moves { get; set; } = new List<Move>();
    }
}

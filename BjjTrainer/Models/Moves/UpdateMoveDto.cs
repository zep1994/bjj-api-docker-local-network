namespace BjjTrainer.Models.Moves
{
    namespace BjjTrainer.Models.DTO.Moves
    {
        public class UpdateMoveDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsSelected { get; set; }  // NEW: Track selection
        }
    }

}

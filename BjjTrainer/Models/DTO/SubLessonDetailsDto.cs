namespace BjjTrainer.Models.DTO
{
    public class SubLessonDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Notes { get; set; }         // Array of tags associated with the sublesson

        // Constructor with optional parameters for easier instantiation in tests or mock data
        public SubLessonDetailsDto(
            int id = 0,
            string title = "Test",
            string content = "Test",
            string notes = ""
            )
        {
            Id = id;
            Title = title;
            Content = content;
            Notes = notes;
        }
    }
}

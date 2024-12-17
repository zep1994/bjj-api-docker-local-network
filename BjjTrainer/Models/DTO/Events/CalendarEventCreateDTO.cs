namespace BjjTrainer.Models.DTO.Events
{
    public class CalendarEventCreateDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ApplicationUserId { get; set; }
        public bool IsAllDay { get; set; }

    }
}

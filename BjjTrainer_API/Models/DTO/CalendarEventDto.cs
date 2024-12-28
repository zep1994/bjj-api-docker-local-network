namespace BjjTrainer_API.Models.DTO
{
    public class CalendarEventDto
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsAllDay { get; set; } // Added
        public string? RecurrenceRule { get; set; } // Added

        public string FormattedDate => StartDate.ToString();
        public string ShortDescription => Description?.Length > 50 ? Description.Substring(0, 50) + "..." : Description;
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.DTO.Calendar
{
    public class CalendarEventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string ApplicationUserId { get; set; }

        // Use nullable DateTime to handle invalid or null dates
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
    }
}

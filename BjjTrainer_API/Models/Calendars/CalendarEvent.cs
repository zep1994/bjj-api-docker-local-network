using BjjTrainer_API.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.Calendars
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        public string Title { get; set; } = "Event";
        public string? Description { get; set; } = "Training";
        // Use nullable DateTime to handle invalid or null dates
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        public required string ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }

}

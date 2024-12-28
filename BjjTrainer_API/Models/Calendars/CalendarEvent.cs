using BjjTrainer_API.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.Calendars
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        public string Title { get; set; } = "Event";
        public string? Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        public bool IsAllDay { get; set; } = false; 
        public string? RecurrenceRule { get; set; }
        public int? SchoolId { get; set; }  
        public School? School { get; set; }

        public ICollection<CalendarEventUser> CalendarEventUsers { get; set; } = [];
        public ICollection<CalendarEventCheckIn> CalendarEventCheckIns { get; set; } = [];
    }
}

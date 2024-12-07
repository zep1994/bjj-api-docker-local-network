using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.DTO
{
    public class CalendarEventCreateDTO
    {
        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        // Use nullable DateTime to handle invalid or null dates
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
    }
}

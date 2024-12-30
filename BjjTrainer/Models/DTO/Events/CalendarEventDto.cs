using System.Text.Json.Serialization;

namespace BjjTrainer.Models.DTO.Events
{
    public class CalendarEventDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("startTime")]
        public string StartTime { get; set; }  // Keep as string for parsing

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("endTime")]
        public string EndTime { get; set; }  // Keep as string for parsing

        public bool IsAllDay { get; set; }
        public int? SchoolId { get; set; }
    }
}

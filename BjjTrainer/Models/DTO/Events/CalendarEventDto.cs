using System.Text.Json.Serialization;

namespace BjjTrainer.Models.DTO.Events
{
    public class CalendarEventDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        // Property to format the date to MM/dd/yyyy
        public string FormattedDate => StartDate.ToString("MM/dd/yyyy");
        public string FormattedEndDate => EndDate.ToString("MM/dd/yyyy");

        // Property to get a truncated description if available
        public string ShortDescription => !string.IsNullOrEmpty(Description) && Description.Length > 50
            ? Description.Substring(0, 50) + "..." // truncate to 50 characters
            : Description;
    }
}

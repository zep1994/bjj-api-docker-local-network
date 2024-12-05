using System.Text.Json.Serialization;

namespace BjjTrainer.Models.Move
{
    public class MoveResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

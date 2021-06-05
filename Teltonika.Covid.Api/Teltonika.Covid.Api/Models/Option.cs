using System.Text.Json.Serialization;

namespace Teltonika.Covid.Api.Models
{
    public class Option
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}

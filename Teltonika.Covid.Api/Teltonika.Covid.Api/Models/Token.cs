using System.Text.Json.Serialization;

namespace Teltonika.Covid.Api.Models
{
    public class Token
    {
        [JsonPropertyName("token")]
        public string AccessToken { get; set; }
    }
}

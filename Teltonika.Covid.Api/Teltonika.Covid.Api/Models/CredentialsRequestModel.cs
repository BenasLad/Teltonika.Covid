using System.Text.Json.Serialization;

namespace Teltonika.Covid.Api.Models
{
    public class CredentialsRequestModel
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }


        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}

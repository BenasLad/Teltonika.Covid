using System;
using System.Text.Json.Serialization;

namespace Teltonika.Covid.Api.Repositories
{
    public class CreateCaseRequest
    {
        [JsonPropertyName("gender")]
        public int Gender { get; set; }

        [JsonPropertyName("ageBracket")]
        public int AgeBracket { get; set; }

        [JsonPropertyName("municipality")]
        public int Municipality { get; set; }

        [JsonPropertyName("confirmationDate")]
        public DateTime ConfirmationDate { get; set; }

        [JsonPropertyName("Y")]
        public string? Y { get; set; }

        [JsonPropertyName("X")]
        public string? X { get; set; }
    }
}
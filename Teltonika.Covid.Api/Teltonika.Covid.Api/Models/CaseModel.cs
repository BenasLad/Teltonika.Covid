using System;
using System.Text.Json.Serialization;

namespace Teltonika.Covid.Api.Models
{
    public class CaseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("age_bracket")]
        public string? AgeBracket { get; set; }

        [JsonPropertyName("municipality")]
        public string? Municipality { get; set; }

        [JsonPropertyName("confirmation_date")]
        public DateTime? ConfirmationDate { get; set; }

        [JsonPropertyName("case_code")]
        public string? CaseCode { get; set; }

        [JsonPropertyName("X")]
        public string? X { get; set; }

        [JsonPropertyName("Y")]
        public string? Y { get; set; }
    }
}

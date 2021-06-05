using System;
using System.Text.Json.Serialization;

namespace Teltonika.Covid.Api.Models
{
    public class FilterOptions
    {
        [JsonPropertyName("gender")]
        public int? Gender { get; set; }

        [JsonPropertyName("ageBracket")]
        public int? AgeBracket { get; set; }

        [JsonPropertyName("municipality")]
        public int? Municipality { get; set; }

        [JsonPropertyName("confirmationDateFrom")]
        public DateTime? ConfirmationDateFrom { get; set; }

        [JsonPropertyName("confirmationDateTo")]
        public DateTime? ConfirmationDateTo { get; set; }
    }
}

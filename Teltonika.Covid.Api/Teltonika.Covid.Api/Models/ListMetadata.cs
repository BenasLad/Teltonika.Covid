using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Teltonika.Covid.Api.Models
{
    public class ListMetadata
    {
        [JsonPropertyName("genders")]
        public IEnumerable<Option>? Genders { get; set; }

        [JsonPropertyName("ageBrackets")]
        public IEnumerable<Option>? AgeBrackets { get; set; }

        [JsonPropertyName("municipalities")]
        public IEnumerable<Option>? Municipalities { get; set; }
    }
}

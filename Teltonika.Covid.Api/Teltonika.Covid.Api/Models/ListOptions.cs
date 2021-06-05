using System.Text.Json.Serialization;

namespace Teltonika.Covid.Api.Models
{
    public class ListOptions
    {
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("filters")]
        public FilterOptions? Filters { get; set; }
    }
}

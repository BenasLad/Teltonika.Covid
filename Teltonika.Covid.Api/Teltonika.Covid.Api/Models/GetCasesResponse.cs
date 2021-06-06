using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Teltonika.Covid.Api.Models
{
    public class GetCasesResponse
    {
        [JsonPropertyName("cases")]
        public IEnumerable<CaseModel> Cases { get; set; }

        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }
    }
}

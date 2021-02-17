using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infrastructure.Api.JsonObjects
{
    public class PagedResultJson<T>
    {
        [JsonPropertyName("page")]
        public int PageNumber { get; set; }

        [JsonPropertyName("total_results")]
        public int AllRows { get; set; }

        [JsonPropertyName("total_pages")]
        public int PagesCount { get; set; }

        [JsonPropertyName("results")]
        public List<T> Results { get; set; }
    }
}

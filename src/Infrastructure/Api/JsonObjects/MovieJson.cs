using System;
using System.Text.Json.Serialization;

namespace Infrastructure.Api.JsonObjects
{
    public class MovieJson
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("vote_average")]
        public double Rating { get; set; }

        [JsonPropertyName("vote_count")]
        public int VotesCount { get; set; }
    }
}

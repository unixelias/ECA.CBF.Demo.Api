using System.Text.Json.Serialization;

namespace ECA.CBF.Demo.Entities
{
    public class TournmentEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tournament-name")]
        public string Name { get; set; }

        [JsonPropertyName("tournament-grade")]
        public string Grade { get; set; }

        [JsonPropertyName("tournament-league")]
        public string League { get; set; }

        [JsonPropertyName("tournament-league-id")]
        public int LeagueId { get; set; }

        [JsonPropertyName("tournament-season")]
        public int Season { get; set; }

    }
}
using System;
using System.Text.Json.Serialization;

namespace ECA.CBF.Demo.Entities
{
    public class CardEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("card-match-id")]
        public int MatchId { get; set; }

        [JsonPropertyName("card-type")]
        public string Type { get; set; }

        [JsonPropertyName("card-description")]
        public string Description { get; set; }

        [JsonPropertyName("card-player-name")]
        public string PlayerName { get; set; }

        [JsonPropertyName("card-player-id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("card-team-name")]
        public string TeamName { get; set; }

        [JsonPropertyName("card-team-id")]
        public int TeamId { get; set; }

        [JsonPropertyName("card-date-time")]
        public DateTime Date { get; set; }
    }
}
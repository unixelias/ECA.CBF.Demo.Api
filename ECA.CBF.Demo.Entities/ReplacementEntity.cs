using System;
using System.Text.Json.Serialization;

namespace ECA.CBF.Demo.Entities
{
    public class ReplacementEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("replacement-match-id")]
        public int MatchId { get; set; }

        [JsonPropertyName("replacement-player-in-name")]
        public string PlayerInName { get; set; }

        [JsonPropertyName("replacement-player-in-id")]
        public int PlayerInId { get; set; }

        [JsonPropertyName("replacement-player-out-name")]
        public string PlayerOutName { get; set; }
        
        [JsonPropertyName("replacement-player-out-id")]
        public int PlayerOutId { get; set; }

        [JsonPropertyName("replacement-team-name")]
        public string TeamName { get; set; }
        
        [JsonPropertyName("replacement-team-id")]
        public int TeamId { get; set; }

        [JsonPropertyName("replacement-date-time")]
        public DateTime Date { get; set; }

    }
}
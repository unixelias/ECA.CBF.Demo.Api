using System;
using System.Text.Json.Serialization;

namespace ECA.CBF.Demo.Entities
{
    public class GoalEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("goal-match-id")]
        public int MatchId { get; set; }

        [JsonPropertyName("goal-player-id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("goal-team-id")]
        public int TeamId { get; set; }

        [JsonPropertyName("goal-date-time")]
        public DateTime Date { get; set; }

    }
}
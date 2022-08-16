using System;
using System.Text.Json.Serialization;

namespace ECA.CBF.Demo.Entities
{
    public class MatchEntity : MatchBaseEntity
    {
        [JsonPropertyName("match-tournment")]
        public string Tournment { get; set; }

        [JsonPropertyName("match-team-host")]
        public string TeamHost { get; set; }

        [JsonPropertyName("match-team-host-goals")]
        public int TeamHostGoals { get; set; }

        [JsonPropertyName("match-team-guest")]
        public string TeamGuest { get; set; }

        [JsonPropertyName("match-team-guest-goals")]
        public int TeamGuestGoals { get; set; }

        [JsonPropertyName("match-referee-name")]
        public string RefereeName { get; set; }
    }
}
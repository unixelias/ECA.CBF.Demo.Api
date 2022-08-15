using System;
using System.Text.Json.Serialization;

namespace ECA.CBF.Demo.Entities
{
    public class MatchEntity
    {
        [JsonPropertyName("match-id")]
        public int Id { get; set; }

        [JsonPropertyName("match-stadium")]
        public string Stadium { get; set; }

        [JsonPropertyName("match-date-provided")]
        public DateTime DateProvided { get; set; }

        [JsonPropertyName("match-date-start")]
        public DateTime? DateStart { get; set; }

        [JsonPropertyName("match-date-start-break")]
        public DateTime? DateStartBreak { get; set; }

        [JsonPropertyName("match-date-end-break")]
        public DateTime? DateStopBreak { get; set; }

        [JsonPropertyName("match-date-end")]
        public DateTime? DateEnd { get; set; }

        [JsonPropertyName("match-tournment")]
        public string Tournment { get; set; }

        [JsonPropertyName("match-tournment-id")]
        public int TournmentId { get; set; }

        [JsonPropertyName("match-team-host")]
        public string TeamHost { get; set; }

        [JsonPropertyName("match-team-host-id")]
        public int TeamHostId { get; set; }

        [JsonPropertyName("match-team-host-goals")]
        public int TeamHostGoals { get; set; }

        [JsonPropertyName("match-team-guest")]
        public string TeamGuest { get; set; }

        [JsonPropertyName("match-team-guest-id")]
        public int TeamGuestId { get; set; }

        [JsonPropertyName("match-team-guest-goals")]
        public int TeamGuestGoals { get; set; }

        [JsonPropertyName("match-referee-name")]
        public string RefereeName { get; set; }

        [JsonPropertyName("match-referee-id")]
        public int RefereeId { get; set; }
    }
}
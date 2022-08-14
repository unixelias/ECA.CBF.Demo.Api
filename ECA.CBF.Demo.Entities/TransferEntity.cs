using System;
using System.Text.Json.Serialization;

namespace ECA.CBF.Demo.Entities
{
    public class TransferEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("player-id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("team-id-in")]
        public int TeamIn { get; set; }

        [JsonPropertyName("team-id-out")]
        public int TeamOut { get; set; }

        [JsonPropertyName("date")]
        public DateTime TransferDate { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
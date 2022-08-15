using System.Text.Json.Serialization;

namespace ECA.CBF.Demo.Entities
{
    public class LeagueEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("league-name")]
        public string Name { get; set; }

        [JsonPropertyName("description-grade")]
        public string Description { get; set; }

    }
}
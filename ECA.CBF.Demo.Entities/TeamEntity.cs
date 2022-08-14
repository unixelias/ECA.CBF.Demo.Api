using System.Text.Json.Serialization;

namespace ECA.CBF.Demo.Entities
{
    public class TeamEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("short-name")]
        public string ShortName { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
    }
}
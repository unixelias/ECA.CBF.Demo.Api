using System.Text.Json.Serialization;

namespace ECA.CBF.Demo.Entities
{
    public class PlayerEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("birth-date")]
        public string BirthDate { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
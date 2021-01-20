using System;
using System.Text.Json.Serialization;

namespace KBS.Cities.Shared.DTO
{
    public class CityEditDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("population")]
        public int Population { get; set; }

        [JsonPropertyName("established")]
        public DateTime Established { get; set; }
    }
}

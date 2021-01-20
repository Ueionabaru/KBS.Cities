using System;
using System.Text.Json.Serialization;

namespace KBS.Cities.Shared.DTO
{
    public class CityDto : CityEditDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}

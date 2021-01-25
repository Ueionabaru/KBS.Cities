using System;

namespace KBS.Cities.Shared.DTO
{
    public class CityPaginationFilterDto : PaginationFilterDto
    {
        public string Name { get; set; } = string.Empty;
        public int PopulationFrom { get; set; } = int.MinValue;
        public int PopulationTo { get; set; } = int.MaxValue;
        public DateTime DateFrom { get; set; } = DateTime.MinValue;
        public DateTime DateTo { get; set; } = DateTime.MaxValue;
    }
}

using KBS.Cities.Shared.Enums;

namespace KBS.Cities.Shared.DTO
{
    public class FilterDto
    {
        public string OrderBy { get; set; } = nameof(CityDto.Population);
        public OrderDirection OrderDirection { get; set; } = OrderDirection.Descending;
    }
}

using KBS.Cities.Shared.Enums;

namespace KBS.Cities.Shared.DTO
{
    public class FilterDto
    {
        public virtual string OrderBy { get; set; } = string.Empty;
        public virtual OrderDirection OrderDirection { get; set; } = OrderDirection.Descending;
    }
}

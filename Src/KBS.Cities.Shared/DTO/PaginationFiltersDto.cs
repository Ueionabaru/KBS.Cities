using System;
using static KBS.Cities.Shared.Constants;

namespace KBS.Cities.Shared.DTO
{
    public class PaginationFiltersDto
    {
        public string Name { get; set; } = string.Empty;
        public int PopulationFrom { get; set; } = int.MinValue;
        public int PopulationTo { get; set; } = int.MaxValue;
        public string DateFrom { get; set; } = DateTime.MinValue.ToString(Date.DefaultFormat);
        public string DateTo { get; set; } = DateTime.MaxValue.ToString(Date.DefaultFormat);
        public int PageSize { get; set; } = Pagination.DefaultPageSize;
        public int PageIndex { get; set; } = Pagination.DefaultPageIndex;
    }
}

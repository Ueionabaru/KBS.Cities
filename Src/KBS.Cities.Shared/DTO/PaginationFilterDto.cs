using System;
using static KBS.Cities.Shared.Constants;

namespace KBS.Cities.Shared.DTO
{
    public class PaginationFilterDto : FilterDto
    {
        public int PageSize { get; set; } = Pagination.DefaultPageSize;
        public int PageIndex { get; set; } = Pagination.DefaultPageIndex;
    }
}

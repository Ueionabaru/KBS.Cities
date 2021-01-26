using static KBS.Cities.Shared.Constants;

namespace KBS.Cities.Shared.DTO
{
    public class PaginationFilterDto : FilterDto
    {
        public virtual int PageSize { get; set; } = Pagination.DefaultPageSize;
        public virtual int PageIndex { get; set; } = Pagination.DefaultPageIndex;
    }
}

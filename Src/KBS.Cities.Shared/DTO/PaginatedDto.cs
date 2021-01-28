using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static KBS.Cities.Shared.Constants;

namespace KBS.Cities.Shared.DTO
{
    public class PaginatedDto<T, TFilter> 
        where T : class
        where TFilter : PaginationFilterDto<T>
    {
        [JsonPropertyName("data")] public List<T> Items { get; set; } = new();
        [JsonPropertyName("filter")] public virtual TFilter Filter { get; set; }
        [JsonPropertyName("page_size")] public virtual int PageSize { get; set; } = Pagination.DefaultPageSize;
        [JsonPropertyName("page_index")] public virtual int PageIndex { get; set; } = Pagination.DefaultPageIndex;
        [JsonPropertyName("total_items")] public virtual int TotalItems { get; set; }
        [JsonPropertyName("total_pages")] public virtual int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        [JsonPropertyName("items")] public virtual int ItemsCount => Items.Count;
        [JsonPropertyName("has_next")] public virtual bool HasNextPage => PageIndex < TotalPages;
        [JsonPropertyName("has_previous")] public virtual bool HasPreviousPage => PageIndex - 1 > 0;
    }
}

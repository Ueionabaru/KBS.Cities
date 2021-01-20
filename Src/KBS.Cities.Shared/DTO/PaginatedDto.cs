using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static KBS.Cities.Shared.Constants;

namespace KBS.Cities.Shared.DTO
{
    public class PaginatedDto<T> where T : class
    {
        [JsonPropertyName("data")] public List<T> Data { get; set; } = new();
        [JsonPropertyName("page_size")] public int PageSize { get; set; } = Pagination.DefaultPageSize;
        [JsonPropertyName("page_index")] public int PageIndex { get; set; } = Pagination.DefaultPageIndex;
        [JsonPropertyName("total_items")] public int TotalItems { get; set; }
        [JsonPropertyName("total_pages")] public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        [JsonPropertyName("items")] public int Items => Data.Count;
        [JsonPropertyName("has_next")] public bool HasNextPage => PageIndex < TotalPages;
        [JsonPropertyName("has_previous")] public bool HasPreviousPage => PageIndex - 1 > 0;
    }
}

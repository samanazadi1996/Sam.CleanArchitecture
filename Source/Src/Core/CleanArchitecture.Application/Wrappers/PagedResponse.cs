using CleanArchitecture.Application.DTOs;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Wrappers;

public class PagedResponse<T> : BaseResult<List<T>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }

    public static PagedResponse<T> Ok(PaginationResponseDto<T> model)
    {
        var result = new PagedResponse<T>
        {
            PageNumber = model.PageNumber,
            PageSize = model.PageSize,
            TotalItems = model.Count,
            Data = model.Data,
            Success = true

        };

        result.TotalPages = result.TotalItems / result.PageSize;

        if (result.TotalItems % result.PageSize > 0) result.TotalPages++;

        return result;
    }
}

using CleanArchitecture.Application.DTOs;
using System;
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
            Success = true,
            Data = model.Data,
            PageNumber = model.PageNumber,
            PageSize = model.PageSize,
            TotalItems = model.Count,
            TotalPages = (int)Math.Ceiling(model.Count / (double)model.PageSize)
        };

        return result;
    }
}

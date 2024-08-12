using CleanArchitecture.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Application.Wrappers;

public class PagedResponse<T> : BaseResult<List<T>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }

    public static PagedResponse<T> Ok(PaginationResponseDto<T> model)
    {
        return new PagedResponse<T>
        {
            Success = true,
            Data = model.Data,
            PageNumber = model.PageNumber,
            PageSize = model.PageSize,
            TotalItems = model.Count,
            TotalPages = (int)Math.Ceiling(model.Count / (double)model.PageSize)
        };
    }

    public new static PagedResponse<T> Failure(Error error)
        => new() { Success = false, Errors = [error] };

    public new static PagedResponse<T> Failure(IEnumerable<Error> errors)
        => new() { Success = false, Errors = errors.ToList() };

    public static implicit operator PagedResponse<T>(PaginationResponseDto<T> model)
        => Ok(model);

    public static implicit operator PagedResponse<T>(Error error)
        => new() { Success = false, Errors = [error] };

    public static implicit operator PagedResponse<T>(List<Error> errors)
        => new() { Success = false, Errors = errors };

}

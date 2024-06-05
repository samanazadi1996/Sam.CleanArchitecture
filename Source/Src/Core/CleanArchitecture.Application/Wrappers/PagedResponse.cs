using CleanArchitecture.Application.DTOs;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Wrappers;

public class PagedResponse<T> : BaseResult<List<T>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PagedResponse()
    {

    }

    public PagedResponse(Error error) : base(error)
    {
    }

    public PagedResponse(PagenationResponseDto<T> model)
    {
        PageNumber = model.PageNumber;
        PageSize = model.PageSize;
        TotalItems = model.Count;
        TotalPages = TotalItems / PageSize;
        if (TotalItems % PageSize > 0) TotalPages++;

        this.Data = model.Data;
        this.Success = true;
    }
}

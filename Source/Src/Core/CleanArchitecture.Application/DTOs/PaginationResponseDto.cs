using System.Collections.Generic;

namespace CleanArchitecture.Application.DTOs;

public class PaginationResponseDto<T>(List<T> data, int count, int pageNumber, int pageSize)
{
    public List<T> Data { get; set; } = data;
    public int Count { get; set; } = count;
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}

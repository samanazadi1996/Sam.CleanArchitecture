namespace CleanArchitecture.Application.Parameters;

public class PaginationRequestParameter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

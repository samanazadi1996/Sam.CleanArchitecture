namespace CleanArchitecture.Application.Parameters;

public class PaginationRequestParameter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public PaginationRequestParameter()
    {
        PageNumber = 1;
        PageSize = 20;
    }
    public PaginationRequestParameter(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize;
    }
}

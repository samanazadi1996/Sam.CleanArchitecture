namespace CleanArchitecture.Application.Parameters;

public class PaginationRequestParameter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public PaginationRequestParameter()
    {
        this.PageNumber = 1;
        this.PageSize = 20;
    }
    public PaginationRequestParameter(int pageNumber, int pageSize)
    {
        this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
        this.PageSize = pageSize;
    }
}

namespace CleanArchitecture.Application.Parameters
{
    public class PagenationRequestParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PagenationRequestParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 20;
        }
        public PagenationRequestParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize;
        }
    }
}

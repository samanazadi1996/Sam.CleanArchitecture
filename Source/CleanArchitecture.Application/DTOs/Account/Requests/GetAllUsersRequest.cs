namespace CleanArchitecture.Application.DTOs.Account.Requests
{
    public class GetAllUsersRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string Name { get; set; }
    }
}

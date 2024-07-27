using CleanArchitecture.Application.Parameters;

namespace CleanArchitecture.Application.DTOs.Account.Requests;

public class GetAllUsersRequest : PaginationRequestParameter
{
    public string Name { get; set; }
}

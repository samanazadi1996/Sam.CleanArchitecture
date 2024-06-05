using CleanArchitecture.Application.Parameters;

namespace CleanArchitecture.Application.DTOs.Account.Requests;

public class GetAllUsersRequest : PagenationRequestParameter
{
    public string Name { get; set; }
}

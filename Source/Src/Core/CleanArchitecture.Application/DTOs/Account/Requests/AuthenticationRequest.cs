namespace CleanArchitecture.Application.DTOs.Account.Requests;

public class AuthenticationRequest
{
    public string UserName { get; set; }

    public string Password { get; set; }
}
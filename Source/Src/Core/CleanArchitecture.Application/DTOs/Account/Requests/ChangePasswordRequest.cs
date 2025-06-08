namespace CleanArchitecture.Application.DTOs.Account.Requests;

public class ChangePasswordRequest
{
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
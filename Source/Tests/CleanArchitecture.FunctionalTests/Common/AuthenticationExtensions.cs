using CleanArchitecture.Application.DTOs.Account.Responses;
using CleanArchitecture.Application.Wrappers;

namespace CleanArchitecture.FunctionalTests.Common;

public static class AuthenticationExtensions
{
    public static async Task<AuthenticationResponse> GetGhostAccount(this HttpClient client)
    {
        var url = ApiRoutes.Account.Start;

        var result = await client.PostAndDeserializeAsync<BaseResult<AuthenticationResponse>>(url);

        return result.Data;
    }
}

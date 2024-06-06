using CleanArchitecture.Application.DTOs.Account.Responses;
using CleanArchitecture.Application.Wrappers;

namespace CleanArchitecture.FunctionalTests.Common;

public static class AuthenticationExtensionMethods
{
    public static async Task<AuthenticationResponse> GetGhostAccount(this HttpClient _client)
    {
        var url = "/api/v1/Account/Start";

        var result = await _client.PostAndDeserializeAsync<BaseResult<AuthenticationResponse>>(url);

        return result.Data;
    }
}

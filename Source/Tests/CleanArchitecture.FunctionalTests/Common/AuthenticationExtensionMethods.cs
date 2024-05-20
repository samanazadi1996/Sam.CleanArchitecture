using CleanArchitecture.Application.DTOs.Account.Requests;
using CleanArchitecture.Application.DTOs.Account.Responses;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.FunctionalTests.Common;
using System.Threading;

namespace CleanArchitecture.FunctionalTests
{
    public static class AuthenticationExtensionMethods
    {

        private static AuthenticationResponse? AdminToken;
        private static readonly SemaphoreSlim semaphoreAdminToken = new SemaphoreSlim(1, 1);
        public static async Task<AuthenticationResponse> GetAdminAccount(this HttpClient _client, bool force = false)
        {
            await semaphoreAdminToken.WaitAsync();
            try
            {
                if (AdminToken is not null && !force) return AdminToken;

                var url = "/api/v1/Account/Authenticate";
                var model = new AuthenticationRequest()
                {
                    UserName = "Admin",
                    Password = "Sam@12345"
                };

                var result = await _client.PostAndDeserializeAsync<BaseResult<AuthenticationResponse>>(url, model);
                AdminToken = result.Data;

                return AdminToken;
            }
            finally
            {
                semaphoreAdminToken.Release();
            }
        }



        private static AuthenticationResponse? GhostAccount;
        private static readonly SemaphoreSlim semaphoreGhostAccount = new SemaphoreSlim(1, 1);
        public static async Task<AuthenticationResponse> GetGhostAccount(this HttpClient _client, bool force = false)
        {
            await semaphoreGhostAccount.WaitAsync();
            try
            {
                if (GhostAccount is not null && !force) return GhostAccount;

                var url = "/api/v1/Account/Start";

                var result = await _client.PostAndDeserializeAsync<BaseResult<AuthenticationResponse>>(url);
                GhostAccount = result.Data;

                return GhostAccount;
            }
            finally
            {
                semaphoreGhostAccount.Release();
            }
        }
    }
}
using CleanArchitecture.Application.DTOs.Account.Requests;
using CleanArchitecture.Application.DTOs.Account.Responses;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.FunctionalTests.Helpers;
using Shouldly;

namespace CleanArchitecture.FunctionalTests.ApiEndpoints
{
    [Collection("AccountFunctionalTests")]
    public class AccountFunctionalTests(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task ReturnsAccountNotFound()
        {
            // Arrange
            var url = "/api/v1/Account/Authenticate";
            var model = new AuthenticationRequest()
            {
                UserName = "Sam_i_x",
                Password = "Sam@123456"
            };

            // Act
            var result = await _client.PostAndDeserializeAsync<BaseResult<AuthenticationResponse>>(url, model);

            // Assert
            result.Success.ShouldBeFalse();
            result.Errors.ShouldNotBeNull();
            result.Errors[0].ErrorCode.ShouldBe(ErrorCode.NotFound);
        }

        [Fact]
        public async Task ReturnsAccountInformation()
        {
            // Arrange
            var url = "/api/v1/Account/Authenticate";
            var model = new AuthenticationRequest()
            {
                UserName = "Admin",
                Password = "Sam@12345"
            };

            // Act
            var result = await _client.PostAndDeserializeAsync<BaseResult<AuthenticationResponse>>(url, model);

            // Assert
            result.Success.ShouldBeTrue();
            result.Errors.ShouldBeNull();
            result.Data.JWToken.ShouldNotBeNull();
            result.Data.UserName.ShouldBe(model.UserName);
            result.Data.IsVerified.ShouldBeTrue();
        }
    }
}

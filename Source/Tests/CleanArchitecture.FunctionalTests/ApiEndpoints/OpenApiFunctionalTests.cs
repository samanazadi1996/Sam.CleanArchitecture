using CleanArchitecture.FunctionalTests.Common;
using System.Net;

namespace CleanArchitecture.FunctionalTests.ApiEndpoints;

[Collection("OpenApiFunctionalTests")]
public class OpenApiFunctionalTests(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient client = factory.CreateClient();

    [Fact]
    public async Task OpenApi_Json_Should_Be_Accessible()
    {
        // Arrange
        var url = ApiRoutes.OpenApi.SwaggerJson;

        // Act
        var response = await client.GetAsync(url);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("\"openapi\"", content);
        Assert.Contains("\"paths\"", content);
    }

}
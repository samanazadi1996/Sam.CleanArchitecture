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
        // Act
        var response = await client.GetAsync("/swagger/v1/swagger.json");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("\"openapi\"", content);
        Assert.Contains("\"paths\"", content);
    }

}
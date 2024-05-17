using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Domain.Products.Dtos;
using Shouldly;
using CleanArchitecture.FunctionalTests.Helpers;

namespace CleanArchitecture.FunctionalTests.ApiEndpoints
{
    [Collection("ProductFunctionalTests")]
    public class ProductFunctionalTests(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task ReturnsPagedResponseProduct()
        {
            // Arrange
            var url = "/api/v1/Product/GetPagedListProduct";

            // Act
            var result = await _client.GetAndDeserializeAsync<PagedResponse<ProductDto>>(url);

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
        }
    }
}

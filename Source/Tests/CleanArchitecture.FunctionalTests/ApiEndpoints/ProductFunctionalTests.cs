using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Domain.Products.DTOs;
using CleanArchitecture.FunctionalTests.Common;
using Shouldly;

namespace CleanArchitecture.FunctionalTests.ApiEndpoints;

[Collection("ProductFunctionalTests")]
public class ProductFunctionalTests(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient client = factory.CreateClient();

    [Fact]
    public async Task GetPagedListProduct_ShouldReturnPagedResponse()
    {
        // Arrange
        var url = ApiRoutes.Product.GetPagedListProduct.AddQueryString("PageNumber", "1").AddQueryString("PageSize", "10");

        // Act
        var result = await client.GetAndDeserializeAsync<PagedResponse<ProductDto>>(url);

        // Assert
        result.Success.ShouldBeTrue();
        result.Data.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetProductById_ShouldReturnProduct()
    {
        // Arrange
        var url = ApiRoutes.Product.GetProductById.AddQueryString("id", "1");

        // Act
        var result = await client.GetAndDeserializeAsync<BaseResult<ProductDto>>(url);

        // Assert
        result.Success.ShouldBeTrue();
        result.Data.ShouldNotBeNull();
        result.Data.Id.ShouldBe(1);
    }

    [Fact]
    public async Task CreateProduct_ShouldReturnProductId()
    {
        // Arrange
        var url = ApiRoutes.Product.CreateProduct;
        var command = new CreateProductCommand
        {
            Name = RandomDataExtensions.RandomString(10),
            Price = RandomDataExtensions.RandomNumber(100000000),
            BarCode = RandomDataExtensions.RandomString(11)
        };
        var ghostAccount = await client.GetGhostAccount();

        // Act
        var result = await client.PostAndDeserializeAsync<BaseResult<long>>(url, command, ghostAccount.JwToken);

        // Assert
        result.Success.ShouldBeTrue();
        result.Data.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task UpdateProduct_ShouldSucceed()
    {
        // Arrange
        var url = ApiRoutes.Product.UpdateProduct;
        var command = new UpdateProductCommand
        {
            Id = 1,
            Name = RandomDataExtensions.RandomString(10),
            Price = RandomDataExtensions.RandomNumber(100000000),
            BarCode = RandomDataExtensions.RandomString(11)
        };
        var ghostAccount = await client.GetGhostAccount();

        // Act
        var result = await client.PutAndDeserializeAsync<BaseResult>(url, command, ghostAccount.JwToken);

        // Assert
        result.Success.ShouldBeTrue();
    }

    [Fact]
    public async Task DeleteProduct_ShouldSucceed()
    {
        // Arrange
        var url = ApiRoutes.Product.DeleteProduct.AddQueryString("id", "3");
        var ghostAccount = await client.GetGhostAccount();

        // Act
        var result = await client.DeleteAndDeserializeAsync<BaseResult>(url, ghostAccount.JwToken);

        // Assert
        result.Success.ShouldBeTrue();
    }

}

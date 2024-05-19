using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Domain.Products.Dtos;
using CleanArchitecture.FunctionalTests.Common;
using Shouldly;

namespace CleanArchitecture.FunctionalTests.ApiEndpoints
{
    [Collection("ProductFunctionalTests")]
    public class ProductFunctionalTests(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient client = factory.CreateClient();

        [Fact]
        public async Task GetPagedListProduct_ShouldReturnPagedResponse()
        {
            // Arrange
            var url = "/api/v1/Product/GetPagedListProduct";

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
            var firstProduct = await GetFirstProduct();
            var url = $"/api/v1/Product/GetProductById?id={firstProduct.Id}";

            // Act
            var result = await client.GetAndDeserializeAsync<BaseResult<ProductDto>>(url);

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Id.ShouldBe(firstProduct.Id);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnProductId()
        {
            // Arrange
            var url = "/api/v1/Product/CreateProduct";
            var command = new CreateProductCommand
            {
                Name = "New Product",
                Price = 100000,
                BarCode = "abcdefgh"
            };
            var token = await client.GetAdminToken();

            // Act
            var result = await client.PostAndDeserializeAsync<BaseResult<long>>(url, command, token);

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task UpdateProduct_ShouldSucceed()
        {
            // Arrange
            var url = "/api/v1/Product/UpdateProduct";
            var firstProduct = await GetFirstProduct();
            var command = new UpdateProductCommand
            {
                Id = firstProduct.Id,
                Name = "Updated Product",
                Price = 2000000,
                BarCode = "qwertyuio"
            };
            var token = await client.GetAdminToken();

            // Act
            var result = await client.PutAndDeserializeAsync<BaseResult>(url, command, token);

            // Assert
            result.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task DeleteProduct_ShouldSucceed()
        {
            // Arrange
            var firstProduct = await GetLastProduct();
            var url = $"/api/v1/Product/DeleteProduct?id={firstProduct.Id}";
            var token = await client.GetAdminToken();

            // Act
            var result = await client.DeleteAndDeserializeAsync<BaseResult>(url, token);

            // Assert
            result.Success.ShouldBeTrue();
        }

        #region private
        private async Task<ProductDto?> GetFirstProduct()
        {
            var url = "/api/v1/Product/GetPagedListProduct";
            var result = await client.GetAndDeserializeAsync<PagedResponse<ProductDto>>(url);
            return result.Data.FirstOrDefault();
        }
        private async Task<ProductDto?> GetLastProduct()
        {
            var url = "/api/v1/Product/GetPagedListProduct";
            var result = await client.GetAndDeserializeAsync<PagedResponse<ProductDto>>(url);
            return result.Data.LastOrDefault();
        }
        #endregion

    }
}

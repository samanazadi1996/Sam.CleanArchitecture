using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Features.Products.Queries.GetPagedListProduct;
using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Domain.Products.DTOs;
using Moq;
using Shouldly;

namespace CleanArchitecture.UnitTests.ApplicationTests.Features.Products.Queries;

public class GetPagedListProductQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsPagedListResponse()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;
        var productName = "Test Product";

        var products = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Product 1", Price = 1000 },
                new ProductDto { Id = 2, Name = "Product 2", Price = 1500 }
            };

        var productRepositoryMock = new Mock<IProductRepository>();
        productRepositoryMock.Setup(repo => repo.GetPagedListAsync(pageNumber, pageSize, productName))
                             .ReturnsAsync(new PaginationResponseDto<ProductDto>(products, 100, pageNumber, pageSize));

        var handler = new GetPagedListProductQueryHandler(productRepositoryMock.Object);

        var query = new GetPagedListProductQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Name = productName
        };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Data.ShouldNotBeNull();
        result.Data.ShouldBeEquivalentTo(products);
        result.PageNumber.ShouldBe(pageNumber);
        result.PageSize.ShouldBe(pageSize);
    }
}

using CleanArchitecture.Domain.Products.Entities;
using Shouldly;

namespace CleanArchitecture.IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
    [Fact]
    public async Task AddProduct_ShouldAddProductSuccessfully()
    {
        // Arrange
        var productName = "Test Product";
        var productPrice = 1000;
        var productBarCode = "123456789";
        var repository = GetRepository<Product>();
        var unitOfWork = GetUnitOfWork();
        var product = new Product(productName, productPrice, productBarCode);

        // Act
        await repository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        // Assert
        var newProduct = (await repository.GetAllAsync()).FirstOrDefault();

        newProduct.ShouldNotBeNull();
        newProduct.Name.ShouldBe(productName);
        newProduct.Price.ShouldBe(productPrice);
        newProduct.BarCode.ShouldBe(productBarCode);
    }

    [Fact]
    public async Task AddProductWithoutSaving_ShouldNotPersistProduct()
    {
        // Arrange
        var productName = "Test Product";
        var productPrice = 1000;
        var productBarCode = "123456789";
        var repository = GetRepository<Product>();
        var product = new Product(productName, productPrice, productBarCode);

        // Act
        await repository.AddAsync(product);

        // Assert
        var newProduct = (await repository.GetAllAsync()).FirstOrDefault();

        newProduct.ShouldBeNull();
    }
}

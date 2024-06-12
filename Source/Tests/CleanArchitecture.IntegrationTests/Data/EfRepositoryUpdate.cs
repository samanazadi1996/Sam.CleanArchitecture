using CleanArchitecture.Domain.Products.Entities;
using Shouldly;

namespace CleanArchitecture.IntegrationTests.Data;

public class EfRepositoryUpdate : BaseEfRepoTestFixture
{
    [Fact]
    public async Task UpdateProduct_ShouldUpdateProductSuccessfully()
    {
        // Arrange
        var productName = "Test Product";
        var productPrice = 1000;
        var productBarCode = "123456789";
        var repository = GetRepository<Product>();
        var unitOfWork = GetUnitOfWork();
        var product = new Product(productName, productPrice, productBarCode);

        await repository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        // Act
        var newProduct = await repository.GetByIdAsync(product.Id);
        newProduct.ShouldNotBeNull();

        newProduct.Update("Updated Product", 1500, "987654123");

        await unitOfWork.SaveChangesAsync();

        // Assert
        var updatedProduct = (await repository.GetAllAsync()).FirstOrDefault();

        updatedProduct.ShouldNotBeNull();
        updatedProduct.Name.ShouldBe("Updated Product");
        updatedProduct.Price.ShouldBe(1500);
        updatedProduct.BarCode.ShouldBe("987654123");
    }

    [Fact]
    public async Task UpdateProductWithoutSaving_ShouldNotPersistUpdate()
    {
        // Arrange
        var productName = "Test Product";
        var productPrice = 1000;
        var productBarCode = "123456789";
        var repository = GetRepository<Product>();
        var unitOfWork = GetUnitOfWork();
        var product = new Product(productName, productPrice, productBarCode);

        await repository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        // Act
        var newProduct = await repository.GetByIdAsync(product.Id);
        newProduct.ShouldNotBeNull();

        newProduct.Update("Updated Product", 1500, "987654123");

        // Assert
        var updatedProduct = (await repository.GetAllAsync()).FirstOrDefault();

        updatedProduct.ShouldNotBeNull();
        updatedProduct.Name.ShouldBe(productName);
        updatedProduct.Price.ShouldBe(productPrice);
        updatedProduct.BarCode.ShouldBe(productBarCode);
    }
}

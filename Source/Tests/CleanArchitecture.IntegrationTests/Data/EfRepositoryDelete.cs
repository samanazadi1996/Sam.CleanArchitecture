using CleanArchitecture.Domain.Products.Entities;
using Shouldly;

namespace CleanArchitecture.IntegrationTests.Data;

public class EfRepositoryDelete : BaseEfRepoTestFixture
{
    [Fact]
    public async Task DeleteProduct_ShouldDeleteProductSuccessfully()
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

        repository.Delete(newProduct);
        await unitOfWork.SaveChangesAsync();

        // Assert
        var deletedProduct = (await repository.GetAllAsync()).FirstOrDefault();

        deletedProduct.ShouldBeNull();
    }

    [Fact]
    public async Task DeleteProductWithoutSaving_ShouldNotPersistDeletion()
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

        repository.Delete(newProduct);

        // Assert
        var deletedProduct = (await repository.GetAllAsync()).FirstOrDefault();

        deletedProduct.ShouldNotBeNull();
    }
}

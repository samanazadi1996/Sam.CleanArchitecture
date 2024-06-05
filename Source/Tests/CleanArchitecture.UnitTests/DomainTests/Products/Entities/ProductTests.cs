using CleanArchitecture.Domain.Products.Entities;
using Shouldly;

namespace CleanArchitecture.UnitTests.DomainTests.Products.Entities;

public class ProductTests
{
    [Fact]
    public void Should_Create_Product_With_Valid_Parameters()
    {
        // Arrange
        var name = "Product1";
        var price = 10.0;
        var barCode = "123456789";

        // Act
        var product = new Product(name, price, barCode);

        // Assert
        product.Name.ShouldBe(name);
        product.Price.ShouldBe(price);
        product.BarCode.ShouldBe(barCode);
    }

    [Fact]
    public void Should_Update_Product_Details()
    {
        // Arrange
        var product = new Product("Product1", 10.0, "123456789");
        var newName = "UpdatedProduct";
        var newPrice = 20.0;
        var newBarCode = "987654321";

        // Act
        product.Update(newName, newPrice, newBarCode);

        // Assert
        product.Name.ShouldBe(newName);
        product.Price.ShouldBe(newPrice);
        product.BarCode.ShouldBe(newBarCode);
    }
}

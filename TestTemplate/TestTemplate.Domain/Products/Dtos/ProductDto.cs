using TestTemplate.Domain.Products.Entities;

namespace TestTemplate.Domain.Products.Dtos
{
    public class ProductDto
    {
        public ProductDto()
        {
        }
        public ProductDto(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            BarCode = product.BarCode;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string BarCode { get; set; }

    }
}

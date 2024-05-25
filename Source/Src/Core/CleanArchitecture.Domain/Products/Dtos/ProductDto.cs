using CleanArchitecture.Domain.Products.Entities;
using System;

namespace CleanArchitecture.Domain.Products.Dtos
{
    public class ProductDto
    {
        public ProductDto(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            BarCode = product.BarCode;
            CreatedDateTime = product.Created;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string BarCode { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}

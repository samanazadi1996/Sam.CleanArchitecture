using $ext_projectname$.Domain.Products.Entities;
using System;

namespace $ext_projectname$.Domain.Products.DTOs
{
    public class ProductDto
    {
#pragma warning disable
        public ProductDto()
        {

        }
#pragma warning restore 
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

using Sam.CleanArchitecture.Domain.Common;
using System;

namespace Sam.CleanArchitecture.Domain.Products.Entities
{
    public class Product : AuditableBaseEntity
    {
        private Product()
        {
        }
        public Product(string name, double price, string barCode)
        {
            Name = name;
            Price = price;
            BarCode = barCode;
        }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string BarCode { get; private set; }

        public void Update(string name, double price, string barCode)
        {
            Name = name;
            Price = price;
            BarCode = barCode;
        }
    }
}

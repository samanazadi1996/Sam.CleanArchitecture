using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Products.Entities
{
    public class Product : AuditableBaseEntity
    {
#pragma warning disable
        private Product()
        {
        }
#pragma warning disable
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

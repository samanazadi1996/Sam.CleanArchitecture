using Sam.CleanArchitecture.Domain.Common;

namespace Sam.CleanArchitecture.Domain.Products.Entities
{
    public class Product : AuditableBaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string BarCode { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sam.CleanArchitecture.Domain.Products.Entities;

namespace Sam.CleanArchitecture.Infrastructure.Persistence.Contexts.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p => p.BarCode).HasMaxLength(50);
        }
    }
}

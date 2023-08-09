using Microsoft.EntityFrameworkCore;
using Sam.CleanArchitecture.Application.Interfaces;
using Sam.CleanArchitecture.Domain.Common;
using Sam.CleanArchitecture.Domain.Products.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IAuthenticatedUserService authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            this.authenticatedUser = authenticatedUser;
        }

        public DbSet<Product> Products { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (authenticatedUser.UserId is not null)
            {
                var userId = Guid.Parse(authenticatedUser.UserId);
                foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.Created = DateTime.Now;
                            entry.Entity.CreatedBy = userId;
                            break;
                        case EntityState.Modified:
                            entry.Entity.LastModified = DateTime.Now;
                            entry.Entity.LastModifiedBy = userId;
                            break;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
        }
    }
}

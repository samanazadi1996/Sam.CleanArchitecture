using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Products.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence.Contexts
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
            var userId = Guid.Parse(authenticatedUser.UserId ?? "00000000-0000-0000-0000-000000000000");
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IAuditableEntity auditableEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditableEntity.SetCreationDetails(userId, DateTime.Now);
                            break;
                        case EntityState.Modified:
                            auditableEntity.SetModificationDetails(userId, DateTime.Now);
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

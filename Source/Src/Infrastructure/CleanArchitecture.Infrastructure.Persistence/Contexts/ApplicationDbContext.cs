using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Products.Entities;
using CleanArchitecture.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUser) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ChangeTracker.ApplyAuditing(authenticatedUser);

        return base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        this.ConfigureDecimalProperties(builder);

        base.OnModelCreating(builder);
    }
}
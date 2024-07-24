# [ASP Dotnet Core Clean Architecture](../README.md) - Soft Delete

## Introduction

Soft Delete is a technique used to mark records as deleted without physically removing them from the database. This approach is beneficial in scenarios where you might need to recover deleted data or keep a historical record of deleted entries. Implementing Soft Delete typically involves adding a boolean flag to the entity model to indicate whether the record is considered deleted. In this documentation, we will guide you through the steps to implement Soft Delete in a Clean Architecture project using Entity Framework Core.


## Getting Started

1. Adding the 'IsDeleted' Property to [BaseEntity](../Source/Src/Core/CleanArchitecture.Domain/Common/BaseEntity.cs)
    - First, add the 'IsDeleted' property to the base class ('BaseEntity'). This property indicates logical deletion (Soft Delete).

    ```c#
    public class BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
        // Other properties and methods
    }
    ```

2. Adding Soft Delete Extensions
    ```c#
    using CleanArchitecture.Domain.Common;
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;

    namespace CleanArchitecture.Infrastructure.Persistence.Contexts.Extensions;

    internal static class SoftDeleteExtensions
    {
        internal static void ApplySoftDeleteQueryFilter(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var isDeletedProperty = entityType.FindProperty("IsDeleted");
                if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var body = Expression.Equal(
                        Expression.Property(parameter, isDeletedProperty.PropertyInfo),
                        Expression.Constant(false));
                    var lambda = Expression.Lambda(body, parameter);

                    builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

        }

        internal static void SoftDeleteEntities(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                }
            }
        }
    }
    ```

3. Modifying the [DbContext](../Source/Src/Infrastructure/CleanArchitecture.Infrastructure.Persistence/Contexts/ApplicationDbContext.cs) Class
    In your 'DbContext' class, override the 'OnModelCreating' and 'SaveChangesAsync' methods to use the Soft Delete extensions.
    ```c#
    public class YourDbContext : DbContext
    {
        public YourDbContext(DbContextOptions<YourDbContext> options) : base(options) { }

        // Other DbSets and configurations

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplySoftDeleteQueryFilter();
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.SoftDeleteEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            this.SoftDeleteEntities();
            return base.SaveChanges();
        }
    }
    ```

4. Testing the Implementation
    To ensure the correct functionality of Soft Delete, perform the following tests:

    1. Adding and then deleting an entity: Verify that the record remains in the database with IsDeleted = true.
    
    2. Retrieving entities: Ensure that entities marked as deleted (IsDeleted = true) do not appear in queries.

## Conclusion
By following these steps, you have successfully implemented Soft Delete in your project. Now, data will be logically deleted and can be recovered if needed. If you have any questions or need further assistance, refer to the documentation or online resources.



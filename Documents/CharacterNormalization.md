# [ASP Dotnet Core Clean Architecture](../README.md) -  Character Normalization for Arabic and Persian in Entity Framework Core

In applications that work with databases, especially those supporting right-to-left languages like Persian and Arabic, data consistency and text normalization become crucial. Due to the visual similarity between some Persian and Arabic characters, data may be entered into the system that appears identical but is actually different in terms of encoding. One common issue arises with the Persian letters "ی" and "ک" versus the Arabic letters "ي" and "ك".

These small differences can cause various issues, such as incorrect search results or comparison errors during data processing. In this article, we will explore the challenges related to this problem and provide a solution for normalizing Arabic and Persian characters in Entity Framework Core.

## The Challenge: Character Differences Between Persian and Arabic

In the Persian language, the letters "ی" and "ک" are distinct from their Arabic counterparts "ي" and "ك". While they may look the same visually, they are different in Unicode encoding. These differences can create problems when performing searches, comparisons, and data storage. For example, if a user searches for a word with the Persian "ی" but the word is stored in the database with the Arabic "ي", the search results may not be accurate.

## Solution: Normalizing Characters in Entity Framework Core

To solve this issue, we can use character normalization. Normalization refers to converting Arabic characters into their Persian equivalents before storing them in the database or before performing search operations. This ensures that all strings are converted to a consistent format, eliminating comparison and search issues.

In Entity Framework Core, we can implement this process automatically at the level of entity changes.

## Implementing Character Normalization in EF Core

To implement normalization, we can define an extension method for the `ChangeTracker` in Entity Framework Core. This method automatically inspects the modified or added entities and normalizes the string properties.

### Code for Character Normalization:

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Reflection;

namespace CleanArchitecture.Infrastructure.Persistence.Extensions
{
    public static class TextNormalizationExtensions
    {
        // Method to normalize string properties of tracked entities
        public static void NormalizeEntityStrings(this ChangeTracker changeTracker)
        {
            // Find added or modified entities
            var changedEntities = changeTracker
                .Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var item in changedEntities)
            {
                // Get string properties that are readable and writable
                var propertyInfos = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                // Apply normalization to string properties
                foreach (var propertyInfo in propertyInfos)
                {
                    var val = propertyInfo.GetValue(item.Entity)?.ToString();
                    if (!string.IsNullOrEmpty(val))
                    {
                        var newVal = val.NormalizeText();
                        if (newVal != val)
                        {
                            propertyInfo.SetValue(item.Entity, newVal);
                        }
                    }
                }
            }
        }

        // Method to normalize characters in a single string
        public static string NormalizeText(this string input)
        {
            return string.IsNullOrEmpty(input) ? input : input.Replace("ي", "ی").Replace("ك", "ک");
        }
    }
}
```

### Explanation of the Code:

1. **`NormalizeEntityStrings` Method**: This method is defined as an extension for `ChangeTracker`. The `ChangeTracker` in Entity Framework Core is responsible for tracking changes in entities. This method finds the entities that have been added or modified, retrieves their string properties, and applies normalization to them.
   
2. **`NormalizeText` Method**: This method normalizes a single string by replacing the Arabic "ي" and "ك" with their Persian equivalents "ی" and "ک".

3. **Practical Application**: Every time data changes (insertions or updates) occur, this normalization will automatically be applied.

### Usage in Applications

This method automatically normalizes strings before they are stored in the database. As a result, all stored data will be consistent, eliminating issues such as incorrect search results or comparison errors.

Additionally, the `NormalizeText` method can be used anywhere in the application to normalize strings before they are displayed to the user or processed in other ways. This approach greatly improves data integrity across the system.

### Example Classes Utilizing Normalization

Here is an example of how normalization can be applied in real-world classes:

#### ApplicationDbContext Class:

```csharp
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Products.Entities;
using CleanArchitecture.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUser) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.ApplyAuditing(authenticatedUser);
            ChangeTracker.NormalizeEntityStrings();
            
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.ConfigureDecimalProperties(builder);
            base.OnModelCreating(builder);
        }
    }
}
```

In the `ApplicationDbContext`, normalization is applied every time changes are saved to the database.

#### ProductRepository Class:

```csharp
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Domain.Products.DTOs;
using CleanArchitecture.Domain.Products.Entities;
using CleanArchitecture.Infrastructure.Persistence.Contexts;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Infrastructure.Persistence.Extensions;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class ProductRepository(ApplicationDbContext dbContext) : GenericRepository<Product>(dbContext), IProductRepository
    {
        public async Task<PaginationResponseDto<ProductDto>> GetPagedListAsync(int pageNumber, int pageSize, string name)
        {
            var query = dbContext.Products.OrderBy(p => p.Created).AsQueryable();
            
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name.NormalizeText()));
            }
            
            return await Paged(
                query.Select(p => new ProductDto(p)),
                pageNumber,
                pageSize);
        }
    }
}
```

In this repository class, the `NormalizeText` method is used to ensure that any search queries are normalized before being executed.

### Conclusion

Normalizing Arabic and Persian characters in applications that support these languages is essential for ensuring data consistency and integrity. By implementing character normalization in Entity Framework Core, we can automatically ensure that all stored data is uniform, preventing issues like incorrect searches or comparison errors.

This approach improves the overall reliability of the system and provides a better user experience by ensuring that all data behaves as expected, regardless of the specific characters used in user input.

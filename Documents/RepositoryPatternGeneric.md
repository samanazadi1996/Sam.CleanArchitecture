# [ASP Dotnet Core Clean Architecture](../README.md) - Repository Pattern - Generic

## Introduction

In modern software development, utilizing the repository pattern for data access proves to be highly beneficial. This pattern abstracts business logic from data access concerns, facilitating testable and maintainable code. In this article, we'll delve into implementing the generic repository pattern along with derived repositories in .NET Core.


In this documentation, we will explore how to automatically register repositories in .NET Core services. This is achieved by utilizing reflection and configuration information to dynamically register all repositories present in the project.

## Implementing the Generic Repository

A generic repository provides reusable operations such as adding, deleting, updating, and retrieving entities of a specific type. Below is how you can implement a generic repository in .NET Core:
``` c#
// Application Layer
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}

// Infrastructure Layer
public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;

    public Repository(DbContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}

```

## Implementing Derived Repositories

Derived repositories extend the functionality of the generic repository for a specific entity type. For instance, let's assume we have an entity named "Product" and we need specific operations for it. Below is how you can implement a derived repository for the "Product" entity:


``` c#
public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    // Other specific methods for Product
}

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        return await _context.Set<Product>().Where(p => p.Name == name).ToListAsync();
    }
    // Implementation of other specific methods for Product
}

```

### Workflow

You can use two methods to register repositories

1. Manual registry of repositories

    You can manually add all the repositories
    ``` c#
    public static class ServiceRegistration
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IProductRepository, ProductRepository>();

        }
        
        //..

    }
    ```

2. Automatic register of all repositories 
  
    In this method, you can use the following code. This method uses reflection to find all repositories derived from generics and register them.

    ``` c#
    public static class ServiceRegistration
    {
        public static void RegisterRepositories(thisIServiceCollection  services)
        {
            services.AddTransient(typeo(IGenericRepository<>), typeo    (GenericRepository<>));
            var interfaceType = typeof(IGenericRepository<>);
            var interfaces = Assembly.GetAssembl(interfaceType).GetTypes()
                .Where(p => p.GetInterface(interfaceTypeName.ToString()) !=     null);
            foreach (var item in interfaces)
            {
                var implimentation = Assembly.GetAssembl(typeof (GenericRepository<>)).GetTypes()
                    .FirstOrDefault(p => p.GetInterface(itemName.ToString())    != null);
                services.AddTransient(item, implimentation);
            }
        }

        //..

    }
    ```
    This approach brings flexibility and reduces code repetition, allowing us to automatically manage the data access layer and benefit from better reusability and testability.


## Conclusion

In this article, we explored how to implement the generic repository pattern and derived repositories in .NET Core. Leveraging these patterns, you can achieve clean and maintainable data access layers, enhancing the scalability and testability of your applications.





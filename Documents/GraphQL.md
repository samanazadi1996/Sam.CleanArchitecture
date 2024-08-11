# [ASP Dotnet Core Clean Architecture](../README.md) - GraphQL

## Introduction

GraphQL, developed by Facebook, is a query language for APIs and a runtime for executing those queries with your existing data. It provides a more efficient and flexible alternative to traditional RESTful APIs by allowing clients to request exactly the data they need. Integrating GraphQL into a Clean Architecture project can enhance the API layer, offering a dynamic and responsive way to interact with your application's data. This document outlines the steps to incorporate GraphQL into a Clean Architecture project.

> ### All the steps mentioned above have been implemented in a separate branch. You can access the branch with all the changes through the following link: [here](https://github.com/samanazadi1996/Sam.CleanArchitecture/tree/GraphQL).

## Getting Started

### Step 1: Create a ClassLibrary 
First, create a library class named `CleanArchitecture.Infrastructure.GraphQL` within the `Infrastructure` layer of your project.

### Step 2: Add the Required NuGet Packages
Next, add the following NuGet packages to your [CleanArchitecture.Infrastructure.GraphQL](https://github.com/samanazadi1996/Sam.CleanArchitecture/blob/GraphQL/Source/Src/Infrastructure/CleanArchitecture.Infrastructure.GraphQL/CleanArchitecture.Infrastructure.GraphQL.csproj) library:

``` xml
<PackageReference Include="HotChocolate.AspNetCore" Version="13.9.11" />
<PackageReference Include="HotChocolate.AspNetCore.Authorization" Version="13.9.11" />
<PackageReference Include="HotChocolate.Data.EntityFramework" Version="13.9.11" />
<PackageReference Include="HotChocolate.Pagination" Version="1.0.1" />
<PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="8.0.1" />
```

These packages are essential for setting up GraphQL with HotChocolate and integrating it with Entity Framework and pagination.


### Step 3: Add the `ServiceRegistration` Class
Then, add the following [ServiceRegistration.cs](https://github.com/samanazadi1996/Sam.CleanArchitecture/blob/GraphQL/Source/Src/Infrastructure/CleanArchitecture.Infrastructure.GraphQL/ServiceRegistration.cs) class to configure GraphQL services in your project:
``` c#
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infrastructure.Identity.Contexts;
using CleanArchitecture.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.GraphQL;

public static class ServiceRegistration
{
    public static IServiceCollection AddGraphQlInfrastructure(this IServiceCollection services)
    {
        services.AddGraphQLServer() // Initialize the GraphQL server with HotChocolate
            .AddTypes(typeof(Query)) // Register the GraphQL query types
            .RegisterDbContext<ApplicationDbContext>() // Register the application database context
            .RegisterDbContext<IdentityContext>() // Register the identity database context
            .RegisterService<IAuthenticatedUserService>() // Register the authenticated user service for dependency injection
            .AddProjections() // Add support for query projections
            .AddFiltering() // Enable filtering capabilities for queries
            .AddSorting() // Enable sorting capabilities for queries
            .AddQueryableCursorPagingProvider() // Add cursor-based pagination support
            .AddAuthorization() // Add authorization capabilities
            .InitializeOnStartup(); // Ensure the GraphQL server is initialized on application startup

        return services;
    }
}
```
### Step 4: Add the `Query` Class
Next, add the following [Query.cs](https://github.com/samanazadi1996/Sam.CleanArchitecture/blob/GraphQL/Source/Src/Infrastructure/CleanArchitecture.Infrastructure.GraphQL/Query.cs) class to define your GraphQL queries:

``` c#
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Products.Entities;
using CleanArchitecture.Infrastructure.Identity.Contexts;
using CleanArchitecture.Infrastructure.Identity.Models;
using CleanArchitecture.Infrastructure.Persistence.Contexts;
using HotChocolate.Authorization;

namespace CleanArchitecture.Infrastructure.GraphQL;

public class Query
{
    [UsePaging(MaxPageSize = 100, IncludeTotalCount = true)] // Enable pagination with a maximum page size and total count
    [UseProjection] // Enable projection for optimizing queries based on requested fields
    [UseFiltering] // Enable filtering on this query
    [UseSorting] // Enable sorting on this query
    public IQueryable<Product> GetProducts(ApplicationDbContext db) => db.Products; // Retrieve all products from the database

    [UsePaging(MaxPageSize = 100, IncludeTotalCount = true)] // Enable pagination with a maximum page size and total count
    [UseProjection] // Enable projection for optimizing queries based on requested fields
    [UseFiltering] // Enable filtering on this query
    [UseSorting] // Enable sorting on this query
    [Authorize] // Require authorization for this query
    public IQueryable<Product> GetUserProducts(ApplicationDbContext db, IAuthenticatedUserService authenticatedUserService)
    {
        var userId = Guid.Parse(authenticatedUserService.UserId); // Get the current user's ID
        return db.Products.Where(p => p.CreatedBy == userId); // Retrieve products created by the current user
    }

    [UsePaging(MaxPageSize = 100, IncludeTotalCount = true)] // Enable pagination with a maximum page size and total count
    [UseProjection] // Enable projection for optimizing queries based on requested fields
    [UseFiltering] // Enable filtering on this query
    [UseSorting] // Enable sorting on this query
    [Authorize] // Require authorization for this query
    public IQueryable<ApplicationUser> GetUsers(IdentityContext db) => db.Users; // Retrieve all users from the identity database
}
```

This `Query` class defines the GraphQL queries for your application, including:

- **GetProducts**: Retrieves all products with support for paging, projection, filtering, and sorting.
- **GetOwnProduct**: Retrieves products created by the authenticated user, with the same features and authorization.
- **GetUsers**: Retrieves all users from the identity context, with authorization required.


### Step 5: Modify `Program.cs`
Next, update the [Program.cs](https://github.com/samanazadi1996/Sam.CleanArchitecture/blob/GraphQL/Source/Src/Presentation/CleanArchitecture.WebApi/Program.cs) file to include the GraphQL infrastructure and configure the endpoints:

1. **Register GraphQL Services**: 
   Add the following line to register the GraphQL services:
   ```c#
   builder.Services.AddGraphQlInfrastructure();   
   ```

2. **Configure Endpoints**:
   Modify the `app.MapControllers();` line to include GraphQL endpoints by replacing it with:
   ```c#
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapGraphQL();
    });
   ```
3. **Access the GraphQL Endpoint**: After running your application, you can access the GraphQL API at the default URL: `/graphql`. This is where you can interact with your GraphQL queries and mutations.


#### Explanation

- **builder.Services.AddGraphQlInfrastructure();**: This line registers the GraphQL infrastructure you set up earlier, ensuring all the necessary GraphQL services are available in your application.

- **app.UseEndpoints(endpoints => { ... });**: This configures the application to map both traditional controllers and GraphQL endpoints, allowing your API to handle requests from both types.

- **GraphQL Endpoint**: The default GraphQL endpoint is accessible at `/graphql`, allowing you to test and interact with your API.

## Conclusion

Integrating GraphQL into an ASP.NET Core Clean Architecture project enhances your API by enabling precise data requests and improving efficiency. By following the outlined steps, you can seamlessly add GraphQL to your project, making your API more dynamic and maintaining a clean project structure.



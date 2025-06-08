using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Products.Entities;
using CleanArchitecture.Infrastructure.Identity.Contexts;
using CleanArchitecture.Infrastructure.Identity.Models;
using CleanArchitecture.Infrastructure.Persistence.Contexts;
using HotChocolate.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using System;
using System.Linq;

namespace CleanArchitecture.Infrastructure.GraphQL;

public class Query
{
    [UsePaging(MaxPageSize = 100, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Product> GetProducts(ApplicationDbContext db) => db.Products;

    [UsePaging(MaxPageSize = 100, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [Authorize]
    public IQueryable<Product> GetUserProducts(ApplicationDbContext db, IAuthenticatedUserService authenticatedUserService)
    {
        var userId = Guid.Parse(authenticatedUserService.UserId);
        return db.Products.Where(p => p.CreatedBy == userId);
    }

    [UsePaging(MaxPageSize = 100, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [Authorize]
    public IQueryable<ApplicationUser> GetUsers(IdentityContext db) => db.Users;
}

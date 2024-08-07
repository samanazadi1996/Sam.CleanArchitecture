# [ASP Dotnet Core Clean Architecture](../README.md) - User Auditing

## Introduction

In web applications, tracking and logging user activities is crucial. User Auditing is one of the essential methods to achieve this, allowing you to track and log user activities effectively. In this article, we will explore a simple implementation of User Auditing using ASP.NET Core, achieved by inheriting from a base class named 'AuditableBaseEntity'.

## The AuditableBaseEntity Class

First, we create a base class called 'AuditableBaseEntity', inheriting from the 'BaseEntity' class. This class includes properties necessary for logging User Auditing information, such as:

``` c#
public abstract class AuditableBaseEntity : BaseEntity
{
    public Guid CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? LastModified { get; set; }
}
```

- CreatedBy: The user ID who created the entity.
- Created: The date and time the entity was created.
- LastModifiedBy: The user ID who last modified the entity.
- LastModified: The date and time the entity was last modified.

This class allows you to automatically register User Auditing information for each entity in your application.

## The ApplicationDbContext Class

In the 'ApplicationDbContext' class, which serves as a DbContext for Entity Framework Core, we override the 'SaveChangesAsync' method. This method is used to track changes in entities and log User Auditing information. Here, we use information provided by a service called 'IAuthenticatedUserService' to determine the user ID. This service provides information about the current user during the program's execution.

``` c#
public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
{
    var userId = string.IsNullOrEmpty(authenticatedUser.UserId)
        ? Guid.Empty : Guid.Parse(authenticatedUser.UserId)

    var currentTime = DateTime.UtcNow

    foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
    {
        if (entry.State == EntityState.Added)
        {
            entry.Entity.Created = currentTime;
            entry.Entity.CreatedBy = userId;
        }
        else if (entry.State == EntityState.Modified)
        {
            entry.Entity.LastModified = currentTime;
            entry.Entity.LastModifiedBy = userId;
        }
    }
    return base.SaveChangesAsync(cancellationToken);
}
```

## Implementation of the AuthenticatedUserService Service
In the 'AuthenticatedUserService' service, we use 'IHttpContextAccessor' to access information about the current user in ASP.NET Core. This information includes the user's ID and name, retrieved from the 'HttpContext' of the current request. Then, this information is sent as output from the service.

``` c#
public class AuthenticatedUserService(IHttpContextAccessor httpContextAccessor) : IAuthenticatedUserService
{
    public string UserId { get; } = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    public string UserName { get; } = httpContextAccessor.HttpContext?.User.Identity?.Name;
}
```

## Conclusion

Utilizing User Auditing in ASP.NET Core applications can help you easily track and log user activities. By employing an implementation similar to what we discussed in this article, you can automatically register User Auditing information for each entity in your application. This can aid in monitoring and tracking user activities, enhancing the security and traceability of your application.
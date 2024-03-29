# [ASP Dotnet Core Clean Architecture](../README.md) - User Auditing

## Introduction

In web applications, tracking and logging user activities is crucial. User Auditing is one of the essential methods to achieve this, allowing you to track and log user activities effectively. In this article, we will explore a simple implementation of User Auditing using ASP.NET Core, achieved by inheriting from a base class named 'AuditableBaseEntity'.

## The AuditableBaseEntity Class

First, we create a base class called 'AuditableBaseEntity', inheriting from the 'BaseEntity' class. This class includes properties necessary for logging User Auditing information, such as:

``` c#
public abstract class AuditableBaseEntity : BaseEntity, IAuditableEntity
{
    public Guid CreatedBy { get; private set; }
    public DateTime Created { get; private set; }
    public Guid? LastModifiedBy { get; private set; }
    public DateTime? LastModified { get; private set; }

    void IAuditableEntity.SetCreationDetails(Guid createdBy, DateTime created)
    {
        Created = created;
        CreatedBy = createdBy;
    }

    void IAuditableEntity.SetModificationDetails(Guid? lastModifiedBy, DateTime? lastModified)
    {
        LastModified = lastModified;
        LastModifiedBy = lastModifiedBy;
    }
}

public interface IAuditableEntity
{
    void SetCreationDetails(Guid createdBy, DateTime created);
    void SetModificationDetails(Guid? lastModifiedBy, DateTime? lastModified);
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
}```

## Implementation of the AuthenticatedUserService Service
In the 'AuthenticatedUserService' service, we use 'IHttpContextAccessor' to access information about the current user in ASP.NET Core. This information includes the user's ID and name, retrieved from the 'HttpContext' of the current request. Then, this information is sent as output from the service.

``` c#
public class AuthenticatedUserService : IAuthenticatedUserService
{
    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        UserName = httpContextAccessor.HttpContext?.User?.Identity.Name;

    }

    public string UserId { get; }
    public string UserName { get; }
}
```

## Conclusion

Utilizing User Auditing in ASP.NET Core applications can help you easily track and log user activities. By employing an implementation similar to what we discussed in this article, you can automatically register User Auditing information for each entity in your application. This can aid in monitoring and tracking user activities, enhancing the security and traceability of your application.

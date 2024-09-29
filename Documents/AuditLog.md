# [ASP Dotnet Core Clean Architecture](../README.md) - Audit Log

In this article, we’ll discuss the implementation of audit log functionality within a Clean Architecture project. The goal was to provide flexibility by allowing the application to store audit logs either in MongoDB or EventStore, depending on the user's preference.

## Overview

Audit logging is essential for tracking changes in critical business entities. In this project, I implemented two separate audit log services—one for MongoDB and the other for EventStore. This gives developers the flexibility to choose their preferred storage mechanism by specifying the log type in the configuration. This implementation is available in the [audit-log](https://github.com/samanazadi1996/Sam.CleanArchitecture/tree/audit-log) branch.

## Audit Log Infrastructure Setup

The setup is done through extension methods in the [ServiceRegistration](https://github.com/samanazadi1996/Sam.CleanArchitecture/blob/audit-log/Source/Src/Infrastructure/CleanArchitecture.Infrastructure.AuditLog/ServiceRegistration.cs) class, which allows the application to conditionally register either MongoDB or EventStore audit logging services based on the [AuditLogType](https://github.com/samanazadi1996/Sam.CleanArchitecture/blob/audit-log/Source/Src/Infrastructure/CleanArchitecture.Infrastructure.AuditLog/ServiceRegistration.cs#L45-L49) specified.

## Here’s how the services are registered:

```c#
public static IServiceCollection AddAuditLogInfrastructure(this IServiceCollection services, IConfiguration configuration)
{
    var writeTo = configuration["AuditLog:WriteTo"]!;

    if (writeTo.Equals("Mongo", StringComparison.OrdinalIgnoreCase))
    {
        var mongoSettings = configuration.GetSection("AuditLog:Mongo").Get<MongoSettings>();
        services.AddSingleton<IAuditLogService>(new MongoAuditLogService(mongoSettings!));
    }
    else if (writeTo.Equals("EventStore", StringComparison.OrdinalIgnoreCase))
    {
        var eventStoreSettings = configuration.GetSection("AuditLog:EventStore").Get<EventStoreSettings>();
        services.AddSingleton<IAuditLogService>(new EventStoreAuditLogService(eventStoreSettings!));
    }

    return services;
}
```

## MongoDB Audit Log Service
For MongoDB, the connection details, database name, and the collection name where audit logs will be stored are configured using the `MongoSettings` class.

```json
{
  "AuditLog": {
    "WriteTo": "Mongo",
    "Mongo": {
      "ConnectionString": "mongodb://root:password@localhost:27017/",
      "DatabaseName": "CleanArchitectureAuditLog",
      "CollectionName": "AuditLog-Collection"
    }
  }
}
```

## EventStore Audit Log Service
Similarly, the EventStore connection is handled with a structured approach, specifying the connection string and stream name through the `EventStoreSettings` class.

```json
{
  "AuditLog": {
    "WriteTo": "EventStore",
    "EventStore": {
      "ConnectionString": "esdb://localhost:2113?tls=false",
      "StreamName": "AuditLog-Stream"
    }
  }
}
```


## Explanation of Connection Strings for MongoDB and EventStore

In the Clean Architecture project, two options for audit logging have been implemented: MongoDB and EventStore. Let’s break down the configuration.


### MongoDB Configuration:

```json
"Mongo": {
  "ConnectionString": "mongodb://root:password@localhost:27017/",
  "DatabaseName": "CleanArchitectureAuditLog",
  "CollectionName": "AuditLog-Collection"
}
```
- **ConnectionString**: Defines how to connect to the MongoDB server.
- **DatabaseName**: The name of the database where audit logs will be stored.
- **CollectionName**: The collection within the database where audit logs will be stored.


### EventStore Configuration

```json
"EventStore": {
  "ConnectionString": "esdb://localhost:2113?tls=false",
  "StreamName": "AuditLog-Stream"
}
```
- **ConnectionString**: The URI to connect to the EventStore server.
- **StreamName**: The stream where audit logs will be stored in EventStore.

---

> **_NOTE:_** _Depending on your project's requirements, you can choose to keep one implementation and remove the other, as it is not necessary to use both methods simultaneously_.
> 
> - _**MongoDB**: Use this option if you prefer a NoSQL database for storing audit logs. This implementation is straightforward and integrates well with applications that require flexible data models._
> 
> - _**EventStore**: This option is suitable for event-driven architectures, allowing you to store and manage events in a structured manner. It's ideal for scenarios where event sourcing and CQRS (Command Query Responsibility Segregation) are utilized._
> 
> _Feel free to select the method that best suits your application's needs!_


## Quick Setup for Audit Log Databases with Docker Compose

To quickly set up the necessary databases for your audit log functionality, use the provided Docker Compose file:

[docker-compose.yml](https://github.com/samanazadi1996/Sam.CleanArchitecture/blob/audit-log/Deploy/docker-compose.auditlog.yml)

Steps to Run:
1. Install Docker: Ensure Docker and Docker Compose are installed on your machine.

2. Download the File: Clone the repository or download the docker-compose.auditlog.yml file.

3. Navigate to the Directory:
   ```sh
   cd path/to/directory
   ```
4. Run the Command:
   ```sh
   docker-compose -f docker-compose.auditlog.yml up
   ```

After running these steps, both MongoDB and EventStore will be ready for your Clean Architecture project!


## Branch Reference
All changes related to this implementation are available in the [audit-log](https://github.com/samanazadi1996/Sam.CleanArchitecture/tree/audit-log) branch. You can explore the code and review the flexibility this approach offers for audit logging in Clean Architecture.

---
This article can be included in your documentation to help team members and collaborators understand how audit logging works in the project and how to configure it based on their needs.

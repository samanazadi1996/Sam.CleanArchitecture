# [ASP Dotnet Core Clean Architecture](../README.md) - Audit Log

In this article, we’ll discuss the implementation of audit log functionality within a Clean Architecture project. The goal was to provide flexibility by allowing the application to store audit logs either in MongoDB or EventStore, depending on the user's preference.

## Overview

Audit logging is essential for tracking changes in critical business entities. In this project, I implemented two separate audit log services—one for MongoDB and the other for EventStore. This gives developers the flexibility to choose their preferred storage mechanism by specifying the log type in the configuration. This implementation is available in the [audit-log](https://github.com/samanazadi1996/Sam.CleanArchitecture/tree/audit-log) branch.

## Audit Log Infrastructure Setup

The setup is done through extension methods in the [ServiceRegistration](https://github.com/samanazadi1996/Sam.CleanArchitecture/blob/audit-log/Source/Src/Infrastructure/CleanArchitecture.Infrastructure.AuditLog/ServiceRegistration.cs) class, which allows the application to conditionally register either MongoDB or EventStore audit logging services based on the [AuditLogType](https://github.com/samanazadi1996/Sam.CleanArchitecture/blob/audit-log/Source/Src/Infrastructure/CleanArchitecture.Infrastructure.AuditLog/ServiceRegistration.cs#L45-L49) specified.

## Here’s how the services are registered:

```c#
public static IServiceCollection AddAuditLogInfrastructure(this IServiceCollection services, IConfiguration configuration, AuditLogType auditLogType)
{
    if (auditLogType == AuditLogType.Mongo)
    {
        return services.AddMongoAuditLogInfrastructure(configuration);
    }

    return services.AddEventStoreAuditLogInfrastructure(configuration);
}
```

## MongoDB Audit Log Service
For MongoDB, the connection string includes the connection details, database name, and the collection name where audit logs will be stored:

```c#
public static IServiceCollection AddMongoAuditLogInfrastructure(this IServiceCollection services, IConfiguration configuration)
{
    var config = configuration.GetConnectionString("MongoAuditLogConnection")!.Split(";");

    var connection = config[0].Trim();
    var databaseName = config[1].Trim();
    var collectionName = config[2].Trim();

    services.AddSingleton<IAuditLogService>(new MongoAuditLogService(connection, databaseName, collectionName));

    return services;
}
```

## EventStore Audit Log Service
Similarly, the EventStore connection is handled with the required stream name, allowing the application to stream audit events efficiently:

```c#
public static IServiceCollection AddEventStoreAuditLogInfrastructure(this IServiceCollection services, IConfiguration configuration)
{
    var config = configuration.GetConnectionString("EventStoreAuditLogConnection")!.Split(";");

    var connectionString = config[0].Trim();
    var streamName = config[1].Trim();

    services.AddSingleton<IAuditLogService>(new EventStoreAuditLogService(connectionString, streamName));

    return services;
}
```

## Enum for Log Type
An enumeration, AuditLogType, is used to switch between the two audit log services:

```c#
public enum AuditLogType
{
    Mongo,
    EventStore
}
```

## Usage
To use this feature in your project, you need to specify the audit log type in the configuration and call the appropriate registration method:

```c#
services.AddAuditLogInfrastructure(Configuration, AuditLogType.Mongo);
```

Alternatively, if you prefer EventStore:

```c#
services.AddAuditLogInfrastructure(Configuration, AuditLogType.EventStore);
```

## Explanation of Connection Strings for MongoDB and EventStore
In your CleanArchitecture project, you've implemented two options for audit logging: MongoDB and EventStore. The connection strings used to configure these services follow specific formats for each database system. Let's break down their structure:

### MongoDB Connection String:

```json
"MongoAuditLogConnection": "mongodb://root:password@localhost:27017/;CleanArchitectureAuditLog;AuditLogCollection"
```

This connection string is used to connect to a MongoDB database and consists of several parts:

1. **mongodb://root@localhost:27017/**: This is the MongoDB connection URI that defines how to connect to the MongoDB server.

2. **CleanArchitectureAuditLog**: This is the database name where audit logs will be stored. In this case, CleanArchitectureAuditLog is the name of the database being used for audit logging.

3. **AuditLogCollection**: This is the collection name within the CleanArchitectureAuditLog database where the actual audit log data will be stored. MongoDB uses collections (similar to tables in relational databases) to store data.

### EventStore Connection String

```json
"EventStoreAuditLogConnection": "esdb://localhost:2113?tls=false;AuditLog-stream"
```
This connection string is used to connect to an EventStore server and consists of the following parts:

1. **esdb://localhost:2113?tls=false**: This is the URI for connecting to the EventStore server.

2. **AuditLog-stream**: This specifies the stream name in EventStore where audit log events will be written. Streams in EventStore are analogous to tables in a relational database, but they are used to store event data in an ordered sequence.

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

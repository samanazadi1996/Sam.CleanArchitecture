# ASP Dotnet Core Clean Architecture

An Implementation of Clean Architecture with ASP.NET Core 8 WebApi. With this Open-Source BoilerPlate Template, you will get access to the world of Loosely-Coupled and Inverted-Dependency Architecture in ASP.NET Core 8 WebApi with a lot of best practices.

# Getting Started

[Download the Extension](https://marketplace.visualstudio.com/items?itemName=SamanAzadi1996.ASPDotnetCoreCleanArchitecture) and install it on your machine. Make sure Visual Studio 2022 is installed on your machine with the latest SDK.

Follow these Steps to get started.

[![](https://raw.githubusercontent.com/samanazadi1996/Sam.CleanArchitecture/master/Documents/Images/NewProject.png)](#)

You Solution Template is Ready!

[![](https://raw.githubusercontent.com/samanazadi1996/Sam.CleanArchitecture/master/Documents/Images/ProjectCreated.png)](#)

Next, open up WebAPI/appsettings.json to change the connection strings. Here you can choose to have multiple DBs for a separation of the IdentityDB or have the same DB for Identity and Application and FileManager.

Finally, build and run the Application.

# Default Roles & Credentials

As soon you build and run your application, default users and roles get added to the database.

Default Roles are as follows.
- Admin

Here are the credentials for the default users.
- UserName - Admin / Password - Sam@12345


You can use these default credentials to generate valid JWTokens at the ../api/v1/Account/Authenticate endpoint.

[![](https://raw.githubusercontent.com/samanazadi1996/Sam.CleanArchitecture/master/Documents/Images/Swagger.png)](#)

# Purpose of this Project
Does it really make sense to Setup your ASP.NET Core Solution everytime you start a new WebApi Project ? Aren't we wasting quite a lot of time in doing this over and over gain?

This is the exact Problem that I intend to solve with this Full-Fledged ASP.NET Core 8 WebApi Solution Template, that also follows various principles of Clean Architecture.

The primary goal is to create a Full-Fledged implementation, that is well documented along with the steps taken to build this Solution from Scratch. This Solution Template will also be available within Visual Studio 2022 (by installing the required Nuget Package / Extension).

Demonstrate Clean Monolith Architecture in ASP.NET Core 8
- This is not a Proof of Concept
- Implementation that is ready for Production
- Integrate the most essential libraries and packages

# Give a Star ⭐️

If you found this Implementation helpful or used it in your Projects, do give it a star. Thanks! Or, If you are feeling really generous, Support the Project with a small contribution!

# Technologies

- ASP.NET Core 8 WebApi
- REST Standards

# Features

- [Clean Architecture](./Documents/CleanArchitecture.md)
- CQRS with MediatR Library
- Entity Framework Core - Code First
- Repository Pattern - Generic
- Serilog
- Swagger UI
- [Response Wrappers](./Documents/ResponseWrappers.md)
- [Healthchecks](./Documents/Healthchecks.md)
- Pagination
- Microsoft Identity with JWT Authentication
- Role based Authorization
- Identity Seeding
- Database Seeding
- Custom Exception Handling Middlewares
- API Versioning
- [Localization (fa / en)](./Documents/Localization.md)
- Fluent Validation
- Complete User Management Module (Register / Authenticate / Change UserName / Change Password)
- User Auditing

# Prerequisites

- Visual Studio 2022 Community and above
- .NET Core 8 SDK and above
- Basic Understanding of Architectures and Clean Code Principles

# Share it!

I have personally not come across a clean implementation on a WebAPI, which is the reason that I started building this up. There are quite a lot of improvements and fixes along the way from the day I started out. Thanks to the community for the support and suggestions. Please share this Repository within your developer community, if you think that this would a difference! Thanks.

# About the Author

### Saman Azadi
- Twitter - [Saman Azadi](https://twitter.com/intent/follow?screen_name=saman_azadi_)
- Linkedin - [Saman Azadi](https://www.linkedin.com/in/saman-azadi/)
- Github - [Saman Azadi](https://github.com/samanazadi1996)

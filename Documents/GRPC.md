# [ASP Dotnet Core Clean Architecture](../README.md) - gRPC

## Introduction

gRPC is a powerful remote procedure call (RPC) framework that allows for high-performance communication between services. Built on HTTP/2 and Protocol Buffers, gRPC is designed to be efficient, language-neutral, and scalable, making it a popular choice for microservices architectures and distributed systems.

In many modern software projects, especially those following the Clean Architecture principles, integrating gRPC can enhance communication between different layers and services by providing a strongly-typed, efficient way to handle requests and responses.

## Implementation Example

For those looking to learn how to integrate gRPC into a project that follows the Clean Architecture pattern, a full implementation of CRUD operations for products has been provided in the [gRPC branch](https://github.com/samanazadi1996/Sam.CleanArchitecture/tree/gRPC) of the repository. This branch includes:

- A complete set of gRPC services and messages for managing product data.
- Implementation of CRUD operations using MediatR, ensuring separation of concerns and adherence to Clean Architecture principles.
- Examples of how to call these gRPC services within the application.

### Infrastructure Layer

The gRPC services are implemented within the Infrastructure layer of the project. You can explore the full implementation, including the service definitions and service registrations, in the following link:

- [Infrastructure Layer - gRPC Services](https://github.com/samanazadi1996/Sam.CleanArchitecture/tree/GRPC/Source/Src/Infrastructure/CleanArchitecture.Infrastructure.GRPC)

### Client Usage

In addition to the service implementation, there is also an example of how to consume these gRPC services within the application. This can be found in the Presentation layer of the project. This section demonstrates how to create a gRPC client and interact with the product services:

- [Presentation Layer - gRPC Client Usage](https://github.com/samanazadi1996/Sam.CleanArchitecture/tree/GRPC/Source/Src/Presentation/GrpcClient)


By examining this branch, you can gain insights into how to effectively incorporate gRPC into your Clean Architecture projects, enabling efficient communication between services while maintaining a clean and maintainable codebase.

You can access the branch and explore the implementation through the following link: [gRPC Branch](https://github.com/samanazadi1996/Sam.CleanArchitecture/tree/gRPC).

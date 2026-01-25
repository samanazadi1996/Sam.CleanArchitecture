# [ASP Dotnet Core Clean Architecture](../README.md) - Functional Tests

## Introduction

In the realm of software development, ensuring the quality and proper functionality of systems is of paramount importance. One common approach to ensuring the correctness of systems is through software testing. Among these, Functional Tests are a type of software testing that scrutinizes the overall behavior and functionality of the system from the perspective of users and their expected behaviors.

## Definition of Concept

Functional Tests represent one of the layers of software testing that assesses the system's functionality and behavior based on user interactions and expected outcomes. They are recognized as a form of Black Box Testing, as they do not concern themselves with internal implementation details of the system but rather focus solely on its observable behavior.

In Functional Testing, various inputs are sent to the system, and their outputs are compared against expected outcomes. This ensures that the system functions correctly and delivers all the expected features.

## Introduction to Helper Classes

In addition to writing test cases, creating helper classes can significantly streamline the process of writing and executing Functional Tests. These classes contain reusable methods and utilities that facilitate common testing tasks, such as interacting with APIs, generating test data, and managing test environments. Let's take a closer look at some of the key helper classes used in Functional Testing

### [CustomWebApplicationFactory](../Source/Tests/CleanArchitecture.FunctionalTests/Common/CustomWebApplicationFactory.cs)
  This class extends the WebApplicationFactory class provided by ASP.NET Core to create a custom test host environment. By configuring the test environment to mimic the production environment, CustomWebApplicationFactory ensures that Functional Tests accurately reflect the behavior of the deployed application.

### [ApiRoutes](../Source/Tests/CleanArchitecture.FunctionalTests/Common/ApiRoutes.cs)
  This class defines the API endpoints used in the application under test. By centralizing endpoint definitions in one location, ApiRoutes promotes consistency and ease of maintenance in test code. For example, the AddQueryString method simplifies the process of adding query parameters to API requests.

### [AuthenticationExtensions](../Source/Tests/CleanArchitecture.FunctionalTests/Common/AuthenticationExtensions.cs)
  This class contains extension methods for the HttpClient class to facilitate authentication-related tasks in Functional Tests. The GetGhostAccount method, for instance, retrieves a ghost account for testing purposes, allowing testers to simulate user authentication without relying on real user credentials.

### [RandomDataExtensions](../Source/Tests/CleanArchitecture.FunctionalTests/Common/RandomDataExtensions.cs)
  This class provides methods for generating random test data, such as random strings and numbers. These methods are invaluable for creating diverse test scenarios and covering edge cases in Functional Tests.

### [HttpClientExtensions](../Source/Tests/CleanArchitecture.FunctionalTests/Common/HttpClientExtensions.cs)
  This class enhances the HttpClient class by providing additional methods to simplify common HTTP operations used in Functional Tests. It includes methods for sending HTTP GET, POST, PUT, and DELETE requests and automatically deserializing JSON responses into specified types. By abstracting common tasks such as setting headers and serializing request bodies, it reduces repetitive code and improves the readability and maintainability of test cases.

  - GetAndDeserializeAsync: Sends an HTTP GET request to the specified URI, optionally including an authorization token, and deserializes the JSON response into a specified type.

  - PostAndDeserializeAsync: Sends an HTTP POST request with a provided model as the request body, optionally including an authorization token, and deserializes the JSON response into a specified type.

  - PutAndDeserializeAsync: Sends an HTTP PUT request with a provided model as the request body, optionally including an authorization token, and deserializes the JSON response into a specified type.

  - DeleteAndDeserializeAsync: Sends an HTTP DELETE request to the specified URI, optionally including an authorization token, and deserializes the JSON response into a specified type.

By leveraging these helper classes, testers can write more efficient and maintainable Functional Tests, leading to improved test coverage and software quality.


## Conclusion

Functional Tests are a critical component of ensuring software quality and reliability. By validating the overall behavior and functionality of the system from a user’s perspective, these tests help identify issues that may not be apparent through unit or integration testing alone.

The helper classes we've discussed, such as ApiRoutes, AuthenticationExtensions, CustomWebApplicationFactory, RandomDataExtensions, and HttpClientExtensions, play a vital role in streamlining the process of writing and executing Functional Tests. They reduce boilerplate code, enhance readability, and ensure consistency across test cases, making the testing process more efficient and maintainable.

By incorporating these practices into your testing strategy, you can achieve more comprehensive test coverage, faster feedback loops, and ultimately deliver higher-quality software. As you continue to develop and refine your Functional Tests, these tools and techniques will help you maintain a robust and reliable testing framework that can adapt to the evolving needs of your application.

Functional Testing is not just about finding bugs; it's about building confidence in your software's ability to meet user expectations and perform reliably in production environments. Embrace these testing practices to ensure your software is not only functional but also user-friendly and resilient.


# [ASP Dotnet Core Clean Architecture](../README.md) - Integration Tests

## Introduction

This project contains integration tests to ensure the correct functioning of various components of the system using [EF Core](https://github.com/dotnet/efcore). The tests cover operations such as adding, deleting, and updating product entities.

## Tools Used

- [Shouldly](https://github.com/shouldly/shouldly)
  - An assertion library that improves the readability and understandability of tests.
- [xUnit](https://github.com/xunit/xunit)
  - A unit testing framework for .NET.

## Code Structure

### [BaseEfRepoTestFixture](../Source/Tests/CleanArchitecture.IntegrationTests/Data/BaseEfRepoTestFixture.cs)
- This base class is used for initial setup and creating instances of the in-memory database.

### [EfRepositoryAdd](../Source/Tests/CleanArchitecture.IntegrationTests/Data/EfRepositoryAdd.cs)
- This class includes tests for adding products to the database.

### [EfRepositoryDelete](../Source/Tests/CleanArchitecture.IntegrationTests/Data/EfRepositoryDelete.cs)
- This class includes tests for deleting products from the database.

### [EfRepositoryUpdate](../Source/Tests/CleanArchitecture.IntegrationTests/Data/EfRepositoryUpdate.cs)
- This class includes tests for updating products in the database.

## Conclusion

This documentation helps you understand the structure and methods of executing integration tests in a project using [EF Core](https://github.com/dotnet/efcore). These tests cover operations such as adding, deleting, and updating product entities, utilizing an in-memory database for fast and independent execution from the environment.






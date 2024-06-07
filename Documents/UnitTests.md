# [ASP Dotnet Core Clean Architecture](../README.md) - Unit Tests

## Introduction

Unit testing is a fundamental practice in software development that helps developers confidently modify code and ensure its correctness. In this article, we will explore how to write unit tests.

## Tools Used

- [Moq](https://github.com/devlooped/moq)
  - A mocking library for .NET that allows developers to simulate the behavior of dependencies.
- [Shouldly](https://github.com/shouldly/shouldly)
  - An assertion library that improves the readability and understandability of tests.
- [xUnit](https://github.com/xunit/xunit)
  - A unit testing framework for .NET.

## Directory Organization

### ApplicationTests
  In this directory, tests related to the Application Layer are placed. These tests typically examine the logic of services and their interaction with interfaces.

### DomainTests
  In this directory, tests related to the Domain Layer are placed. These tests typically examine entities and domain logic.


## Conclusion

Using Moq, Shouldly, and xUnit for writing unit tests in Clean Architecture projects helps improve the maintainability and testability of code. By adhering to the principles of Clean Architecture and utilizing proper testing tools, we can ensure the high quality of our software.

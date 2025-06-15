# [ASP Dotnet Core Clean Architecture](../README.md) - Frequently Asked Questions

## Introduction

Welcome to the Frequently Asked Questions (FAQ) section of the ASP.NET Core Clean Architecture project. This document aims to provide answers to the most common questions you may have while using or contributing to this project. Here, you'll find information on various topics, including adding new features, writing unit tests, updating the database, and understanding the project's architecture.

Our goal is to make it easier for you to navigate the project and find the information you need quickly. Whether you're a new contributor or an experienced developer, this FAQ will serve as a valuable resource to help you efficiently work with the Clean Architecture principles implemented in this project.

Please refer to the links and sections below for detailed answers and further documentation.

<details>
<summary><strong>
What is Clean Architecture?
</strong></summary>


For more detailed instructions, you can refer to [this](./CleanArchitecture.md) documentation.
</details>
<br>

<details>
<summary><strong>
How do I install the CleanArchitecture package?
</strong></summary>

For more detailed instructions, you can refer to [this](../README.md) documentation.
</details>
<br>

<details>
<summary><strong>
Does this package have a Visual Studio extension version?
</strong></summary>

Yes, this package has a Visual Studio extension version that can be installed on Visual Studio 2022. You can download and install it from this [link](https://marketplace.visualstudio.com/items?itemName=SamanAzadi1996.ASPDotnetCoreCleanArchitecture).
</details>
<br>


<details>
<summary><strong>
Does this package have a NuGet version?
</strong></summary>

Yes, this package has a NuGet version, and you can download and install it from this [link](https://www.nuget.org/packages/Sam.CleanArchitecture.Template).
</details>
<br>

<details>
<summary><strong>
How do I add use cases, entities, and additional languages using CLI?
</strong></summary>
  
For more detailed instructions, you can refer to [this](./CleanArchitectureTemplates.md) documentation.
</details>
<br>


<details>
  <summary>
    <strong>
How do I modify the database using the tools provided by this package?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./ConfigureDatabase.md) documentation.
</details>
<br>

<details>
  <summary>
    <strong>
How do I apply migrations using the tools provided by this package?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./EasyAddMigrationTools.md) documentation.
</details>
<br>

<details>
  <summary>
    <strong>
How can I best integrate GraphQL into my project?
    </strong>
  </summary>


You can find an article on this topic at this [link](./GraphQL.md), and the sample project for the article is available in [this branch](https://github.com/samanazadi1996/Sam.CleanArchitecture/tree/GraphQL).
</details>
<br>

<details>
  <summary>
    <strong>
How can I best integrate gRPC into my project?
    </strong>
  </summary>

You can find an article on this topic at this [link](./GRPC.md), and the sample project for the article is available in [this branch](https://github.com/samanazadi1996/Sam.CleanArchitecture/tree/GRPC).
</details>
<br>

<details>
<summary><strong>How do I add Rate Limiting to my project?</strong></summary>

To add Rate Limiting to your Clean Architecture project, you can follow the steps outlined in the [Rate Limiting article](./RateLimiting.md). This article provides a comprehensive guide, including how to implement the Rate Limiting class, register it in the `Program.cs` file, and apply it to specific controller actions.

The implementation has been completed in the [Rate Limiting branch of the Sam.CleanArchitecture repository](https://github.com/samanazadi1996/Sam.CleanArchitecture/tree/rete-linit).
</details>
<br>

<details>
  <summary>
    <strong>
How is exception handling implemented?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./ExceptionHandlingMiddlewares.md) documentation.
</details>
<br>


<details>
<summary><strong>
How do I implement OutputCache in this project?
</strong></summary>

For more detailed instructions, you can refer to [this](./OutputCache.md) documentation.
</details>
<br>


<details>
  <summary>
    <strong>
How is Functional Tests implemented?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./FunctionalTests.md) documentation.
</details>
<br>


<details>
  <summary>
    <strong>
How is Unit Tests implemented?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./UnitTests.md) documentation.
</details>
<br>


<details>
  <summary>
    <strong>
How is Integration Tests implemented?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./IntegrationTests.md) documentation.
</details>
<br>

<details>
  <summary>
    <strong>
How do I add Soft Delete?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./SoftDelete.md) documentation.
</details>
<br>

<details>
  <summary>
    <strong>
How is User Auditing implemented?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./UserAuditing.md) documentation.
</details>
<br>
<details>
  <summary>
    <strong>
How can I normalize character input in EF Core entities?
    </strong>
  </summary>

In EF Core, it's common to have data inconsistency when dealing with character variations, especially in multilingual systems. To ensure consistency, character normalization can be applied before saving entities to the database.

For example, you might need to normalize Persian or Arabic characters like "ي" to "ی" or "ك" to "ک". This can be done using EF Core's `ChangeTracker` to normalize string properties before data is persisted to the database.

For a detailed guide and implementation on how to normalize characters in EF Core, you can refer to [this document](./CharacterNormalization.md).
</details>
<br>

<details>
  <summary>
    <strong>
How is Audit Log integrated into the project?
    </strong>
  </summary>

The Audit Log feature allows you to track and store changes made to your application's critical entities. In this project, you can choose between two storage options for audit logs: MongoDB or EventStore, providing flexibility based on your system's requirements.

For a comprehensive guide on configuring and using the audit logging feature, refer to the [Audit Log documentation](./AuditLog.md).
</details>
<br>


<details>
  <summary>
    <strong>
How is Database Seeding implemented?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./DatabaseSeeding.md) documentation.
</details>
<br>

<details>
  <summary>
    <strong>
How is Identity Seeding implemented?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./IdentitySeeding.md) documentation.
</details>
<br>


<details>
  <summary>
    <strong>
How is Localization implemented?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./Localization.md) documentation.
</details>
<br>


<details>
  <summary>
    <strong>
How do I use Google Translate for localization messages?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./Localization.GoogleTranslator.md) documentation.
</details>
<br>

<details>
  <summary>
    <strong>
How is Healthchecks implemented?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./Healthchecks.md) documentation.
</details>
<br>

<details>
  <summary>
    <strong>
How are Response Wrappers used in this project?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./ResponseWrappers.md) documentation.
</details>
<br>

<details>
  <summary>
    <strong>
How do I add a new repository?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./RepositoryPatternGeneric.md) documentation.
</details>
<br>



<details>
  <summary>
    <strong>
How do I run the project with Docker?
    </strong>
  </summary>

For more detailed instructions, you can refer to [this](./DockerDeployment.md) documentation.
</details>
<br>

# [ASP Dotnet Core Clean Architecture](../README.md) - Clean Architecture Templates

## Introduction

In the realm of software development, speed and efficiency are vital factors that significantly impact the quality and success of a project. Furthermore, having access to tools and processes that assist developers in rapidly and effectively creating and managing their code is of utmost importance.

This article aims to explore and introduce the new features recently added to version 8.0.2 and onwards of a dotnet template named "Sam.CleanArchitecture.Template." These features include the ability to create new UseCases, generate new Entities, and add new languages to the project. By leveraging these capabilities, developers will be able to swiftly and proficiently build and maintain various parts of their projects.


## Getting Started

### Create new UseCase
You can create use cases (commands or queries) by navigating to './Src/Core/CleanArchitecture.Application' and running 'dotnet new ca-use-case'. Here are some examples:

#### To create a new command:
``` sh
dotnet new ca-use-case --feature-name Orders --usecase-name CreateOrder --usecase-type command --return-type int
```

#### To create a query:
``` sh
dotnet new ca-use-case -fn Orders -ut query -un GetOrders  -rt "List<Order>"
```

#### To create a querypagedlist:
``` sh
dotnet new ca-use-case -fn Orders -ut querypagedlist -un GetOrders  -rt Order
```

#### To learn more, run the following command:
```
dotnet new ca-use-case --help
```

Short Names
```
-fn : --feature-name
-ut : --usecase-type 
-un : --usecase-name 
-rt : --return-type
```

---

### Create new Entity
You can create entity by navigating to './Src/Core/CleanArchitecture.Domain' and running 'dotnet new ca-entity'. Here are some examples:

#### To create a new entity:
``` sh
dotnet new ca-entity --domain-name Orders --entity-name Order
```

#### To learn more, run the following command:
```
dotnet new ca-entity --help
```

Short Names
```
-dn : --domain-name
-en : --entity-name 
```

---

### Create New Resource

You can create resource by navigating to './Src/Infrastructure/CleanArchitecture.Infrastructure.Resources' and running 'dotnet new ca-resource'. Here are some examples:

#### To create a new resource:
``` sh
dotnet new ca-resource --culture Ar
```

#### To learn more, run the following command:
```
dotnet new ca-resource --help
```

Short Names
```
-c : --culture
```



## Conclusion

In this article, we explored the new features recently added to the Clean Architecture template in ASP Dotnet Core. These features include the ability to create new UseCases, generate new Entities, and add new languages to the project.

By leveraging these capabilities, developers can increase the speed of development and improve the quality of their code. Creating new UseCases using simple and fast commands can expedite development, while adding new Entities and languages provides opportunities for project expansion and flexibility.

Overall, these features empower developers to make significant improvements in the development and management processes of their projects. By harnessing these powerful tools, we can achieve better speed in reaching our software development goals.


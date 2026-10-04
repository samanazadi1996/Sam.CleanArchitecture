# [ASP Dotnet Core Clean Architecture](../README.md) - Clean Architecture Templates

## Introduction

In the realm of software development, speed and efficiency are vital factors that significantly impact the quality and success of a project. Furthermore, having access to tools and processes that assist developers in rapidly and effectively creating and managing their code is of utmost importance.

This article explores the command line templates that ship with the dotnet template package named "Sam.CleanArchitecture.Template". These templates let you create new UseCases (commands and queries), generate new Entities and add new languages to your project with a single command. By leveraging these capabilities, developers will be able to swiftly and proficiently build and maintain various parts of their projects.


## Getting Started

### Create new UseCase

A use case is a single CQRS request of your application: a **command** (it changes state) or a **query** (it only reads state). Every use case lives in the Application layer, inside a folder named after the feature it belongs to, and is made of a request, a handler and – in the case of a command – a FluentValidation validator.

The templates are installed together with the solution package:

``` sh
dotnet new install Sam.CleanArchitecture.Template
```

Run the template **from inside the Application project** so the generated namespaces follow the root namespace of your project:

``` sh
cd Src/Core/CleanArchitecture.Application
```

> The code samples below use the default `CleanArchitecture` prefix. If you created your solution with `dotnet new ca-api -n MyProject`, this prefix is replaced by your project name (for example `MyProject.Application.Features.Orders...`).

#### To create a new command

A command changes state, therefore it usually returns the id of the created record (or nothing at all). Three files are generated: the request, the handler and the validator.

``` sh
dotnet new ca-use-case -fn Orders -ut command -un CreateOrder -rt long
```

The following files are created:

```plaintext
Features/Orders/Commands/CreateOrder/
├── CreateOrderCommand.cs
├── CreateOrderCommandHandler.cs
└── CreateOrderCommandValidator.cs
```

`CreateOrderCommand.cs`:

``` c#
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Wrappers;

namespace CleanArchitecture.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<BaseResult<long>>
    {
        public long MyProperty { get; set; }
    }
}
```

`CreateOrderCommandHandler.cs`:

``` c#
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, BaseResult<long>>
{
    public async Task<BaseResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Handler

        return request.MyProperty;
    }
}
```

`CreateOrderCommandValidator.cs`:

``` c#
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator(ITranslator translator)
    {
        RuleFor(p => p.MyProperty)
            .NotNull()
            .WithName(p => translator[nameof(p.MyProperty)]);
    }
}
```

#### To create a query

A query only reads data, so no validator is generated – a request and a handler are enough.

``` sh
dotnet new ca-use-case -fn Orders -ut query -un GetOrderById -rt OrderDto
```

```plaintext
Features/Orders/Queries/GetOrderById/
├── GetOrderByIdQuery.cs
└── GetOrderByIdQueryHandler.cs
```

``` c#
public class GetOrderByIdQuery : IRequest<BaseResult<OrderDto>>
{
    public OrderDto MyProperty { get; set; }
}
```

#### To create a querypagedlist

Use `querypagedlist` when the query returns a page of data. The generated query inherits from `PaginationRequestParameter` (which adds `PageNumber` and `PageSize`) and responds with `PagedResponse<T>`:

``` sh
dotnet new ca-use-case -fn Orders -ut querypagedlist -un GetOrder -rt OrderDto
```

```plaintext
Features/Orders/Queries/GetOrderPagedList/
├── GetOrderPagedListQuery.cs
└── GetOrderPagedListQueryHandler.cs
```

``` c#
public class GetOrderPagedListQuery : PaginationRequestParameter, IRequest<PagedResponse<OrderDto>>
{
    public OrderDto MyProperty { get; set; }
}

public class GetOrderPagedListQueryHandler : IRequestHandler<GetOrderPagedListQuery, PagedResponse<OrderDto>>
{
    public async Task<PagedResponse<OrderDto>> Handle(GetOrderPagedListQuery request, CancellationToken cancellationToken)
    {
        // Handler

        List<OrderDto> data = [];
        int totalCount = 100;

        return new PaginationResponseDto<OrderDto>(data, totalCount, request.PageNumber, request.PageSize);
    }
}
```

Notice that the generated name gets the `PagedList` suffix, so `-un GetOrder` creates `GetOrderPagedListQuery`.

#### Template options

| Short Name | Option | Required | Description |
| --- | --- | --- | --- |
| -fn | --feature-name | yes | The feature (module) the use case belongs to. It becomes the `Features/{FeatureName}` folder. |
| -ut | --usecase-type | yes | The kind of use case to create: `command`, `query` or `querypagedlist`. |
| -un | --usecase-name | yes | The name of the use case. It becomes the folder and the file names. |
| -rt | --return-type | no (default: `object`) | Replaces the `object` placeholder in the generated request/response types. |

The standard template options are supported as well, for example `-o` to choose the output folder, `--dry-run` to preview the files without creating them and `--force` to overwrite existing files.

#### To learn more, run the following command:
```
dotnet new ca-use-case --help
```

#### After the generation

**1. Replace the `MyProperty` placeholder**

`--return-type` is a plain text replacement of the `object` placeholder, so it also changes the type of the generated `MyProperty`. Replace that property with the real properties of your request, and adjust the generated validation rules accordingly:

``` c#
public class CreateOrderCommand : IRequest<BaseResult<long>>
{
    public string CustomerName { get; set; }
    public decimal Total { get; set; }
}
```

**2. Implement the handler**

The handler is where your business logic lives. It only depends on the abstractions of the Application layer (repositories and the unit of work), which keeps it testable:

``` c#
public class CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, BaseResult<long>>
{
    public async Task<BaseResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerName, request.Total);

        await orderRepository.AddAsync(order);
        await unitOfWork.SaveChangesAsync();

        return order.Id;
    }
}
```

Queries typically return a DTO, and report a missing record by returning an `Error`:

``` c#
public class GetOrderByIdQueryHandler(IOrderRepository orderRepository, ITranslator translator) : IRequestHandler<GetOrderByIdQuery, BaseResult<OrderDto>>
{
    public async Task<BaseResult<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.Id);

        if (order is null)
        {
            return new Error(ErrorCode.NotFound, translator.GetString(TranslatorMessages.OrderMessages.Order_NotFound_with_id(request.Id)), nameof(request.Id));
        }

        return new OrderDto(order);
    }
}
```

`BaseResult`, `BaseResult<TData>` and `PagedResponse<T>` have implicit conversions, so you can return the data or an `Error` directly. For more details, see the [Response Wrappers](./ResponseWrappers.md) documentation. Messages such as `TranslatorMessages.OrderMessages.Order_NotFound_with_id` are declared in `TranslatorMessages` and translated through the resource files of the project, see [Localization](./Localization.md). If your use case needs its own repository, see [Repository Pattern - Generic](./RepositoryPatternGeneric.md).

**3. Validation runs automatically**

Validators are discovered by `AddValidatorsFromAssembly(...)` inside `AddApplicationLayer()` and executed by the `ValidationBehavior` pipeline before the handler runs, so you only have to write the rules. A failed validation is returned as a `400 Bad Request` response by the `ErrorHandlerMiddleware`. Using `ITranslator` in your rules gives you localized error messages, see [Localization](./Localization.md).

**4. Expose the use case over HTTP**

Create an endpoint group in `Src/Presentation/CleanArchitecture.WebApi/Endpoints` and dispatch your use case with `IMediator`:

``` c#
using CleanArchitecture.Application.DTOs.Order.Responses;
using CleanArchitecture.Application.Features.Orders.Commands.CreateOrder;
using CleanArchitecture.Application.Features.Orders.Queries.GetOrderPagedList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Endpoints;

public class OrdersEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {
        builder.MapPost(CreateOrder).RequireAuthorization();

        builder.MapGet(GetOrderPagedList);
    }

    async Task<BaseResult<long>> CreateOrder(IMediator mediator, CreateOrderCommand model)
        => await mediator.Send<CreateOrderCommand, BaseResult<long>>(model);

    async Task<PagedResponse<OrderDto>> GetOrderPagedList(IMediator mediator, [AsParameters] GetOrderPagedListQuery model)
        => await mediator.Send<GetOrderPagedListQuery, PagedResponse<OrderDto>>(model);
}
```

A few things to keep in mind:

- The route is built from the class name and the method name, so the endpoint above is served at `/api/Orders/CreateOrder` and `/api/Orders/GetOrderPagedList`.
- Add `.RequireAuthorization()` to protect an endpoint with the JWT authentication of the solution.
- Queries whose values come from the query string (paging and filtering) are marked with `[AsParameters]`, while commands are bound from the JSON body.
- Endpoint groups are discovered by `app.MapEndpoints()`, and the new endpoint shows up in Swagger without any extra configuration.

**5. There is nothing to register by hand**

Once the files are generated, everything is wired up through assembly scanning:

| What | Where it happens |
| --- | --- |
| Request handlers | `AddMediator()` scans the Application assembly (see `MediatorExtensions`). The application fails fast at startup if a request has no handler. |
| Validators | `AddValidatorsFromAssembly(...)` in `AddApplicationLayer()` (see `ServiceRegistration`). |
| Endpoints | `app.MapEndpoints()` scans the endpoint groups (see `EndpointExtensions`). |

**6. Add tests**

A new use case is ready to be covered by the existing test projects: add the route to `ApiRoutes` in the functional tests project and write a functional test for it (see [Functional Tests](./FunctionalTests.md)), plus a unit test for the handler (see [Unit Tests](./UnitTests.md)).

``` c#
internal static class Order
{
    internal const string CreateOrder = "/api/Orders/CreateOrder";
    internal const string GetOrderPagedList = "/api/Orders/GetOrderPagedList";
}
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

In this article, we explored the command line templates that ship with the Clean Architecture template in ASP Dotnet Core: creating new UseCases, generating new Entities and adding new languages to the project.

By leveraging these capabilities, developers can increase the speed of development and improve the quality of their code. Creating new UseCases using simple and fast commands can expedite development, while adding new Entities and languages provides opportunities for project expansion and flexibility. The generated UseCases only contain the request, the handler and the validator, so you can go from an empty feature folder to a tested and documented endpoint by filling in your business logic and letting the project conventions (dependency injection, validation, response wrappers and Swagger) take care of the rest.

Overall, these features empower developers to make significant improvements in the development and management processes of their projects. By harnessing these powerful tools, we can achieve better speed in reaching our software development goals.


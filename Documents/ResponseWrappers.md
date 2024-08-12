# [ASP Dotnet Core Clean Architecture](../README.md) - Response Wrappers

## Introduction

In software development, robust error handling and consistent result communication are essential for building reliable and maintainable applications. This article explores the design and implementation of a result wrapper in C# to streamline the handling of operation outcomes and error reporting within the context of ASP.NET Core Clean Architecture.


## The BaseResult Class

The `BaseResult` class serves as the foundation for handling the results of operations. It includes the following key components:

```c#
public class BaseResult
{
    public bool Success { get; set; }
    public List<Error> Errors { get; set; }

    public static BaseResult Ok()
        => new() { Success = true };

    public static BaseResult Failure()
        => new() { Success = false };

    public static BaseResult Failure(Error error)
        => new() { Success = false, Errors = [error] };

    public static BaseResult Failure(IEnumerable<Error> errors)
        => new() { Success = false, Errors = errors.ToList() };

    public static implicit operator BaseResult(Error error)
        => Failure(error);

    public BaseResult AddError(Error error)
    {
        Errors ??= [];
        Errors.Add(error);
        Success = false;
        return this;
    }
}
```

1. **Success Property**
   - A boolean property, `Success`, indicates whether the operation was successful (true) or encountered an issue (false).
   - 

2. **Errors Property**
   - The `Errors` property is a list of Error objects that capture details about any errors that occurred during the operation. This standardized approach allows for consistent error reporting.

3. **Static Methods**
   - The class provides static methods to create instances representing successful or failed outcomes.
        1. **Ok()**: Returns a successful result.
        2. **Failure()**: Returns a generic failure result.
        3. **Failure(Error error)**: Returns a failure result with a single error.
        4. **Failure(IEnumerable<Error> errors)**: Returns a failure result with multiple errors.

4. **Implicit Conversion**
    - Implicit conversion from Error to BaseResult is supported for convenience.

5. **AddError Method**
    - The AddError method allows adding an error to an existing result, automatically marking the result as a failure.

## The BaseResult<TData> Generic Class

Building on the foundation of BaseResult, the BaseResult<TData> class introduces generics to handle data associated with successful operations. Key elements include:

```c#
public class BaseResult<TData> : BaseResult
{
    public TData Data { get; set; }

    public static BaseResult<TData> Ok(TData data)
        => new() { Success = true, Data = data };

    public new static BaseResult<TData> Failure(Error error)
        => new() { Success = false, Errors = [error] };

    public new static BaseResult<TData> Failure(IEnumerable<Error> errors)
        => new() { Success = false, Errors = errors.ToList() };

    public static implicit operator BaseResult<TData>(Error error)
        => Failure(error);

    public static implicit operator BaseResult<TData>(TData data)
        => Ok(data);
}
```

1. **Data Property**
   - The `Data` property, of type TData, stores the actual data produced by a successful operation. This allows for flexible handling of different data types.

2. **Static Methods**
   - Similar to the base class, static methods are provided for handling successful operations with or without associated data, as well as for reporting errors.

3. **Implicit Conversions**
   - Implicit conversions from Error to `BaseResult<TData>` and from TData to `BaseResult<TData>` are supported for seamless usage.


## The Error Class

The `Error` class represents an individual error and contains the following properties:

```c#
public class Error(ErrorCode errorCode, string description = null, string fieldName = null)
{
    public ErrorCode ErrorCode { get; set; } = errorCode;
    public string FieldName { get; set; } = fieldName;
    public string Description { get; set; } = description;
}
```

1. **ErrorCode Property**
   - An enumeration, `ErrorCode`, categorizes errors into predefined types. This enables developers to identify and handle specific error scenarios systematically.

2. **FieldName and Description Properties**
   - These properties allow for additional context about the error, specifying the field or area where the error occurred and providing a human-readable description.

## The ErrorCode Enumeration

The `ErrorCode` enumeration defines a set of error codes that cover various error scenarios, providing a standardized approach to categorizing and handling errors.

```c#
public enum ErrorCode : short
{
    ModelStateNotValid = 0,
    FieldDataInvalid = 1,
    NotFound = 2,
    AccessDenied = 3,
    ErrorInIdentity = 4,
    Exception = 5,
}
```
## The PagedResponse Class

The `PagedResponse<T>` class extends the `BaseResult<List<T>>`, introducing properties specifically tailored for paginated responses:

```c#
public class PagedResponse<T> : BaseResult<List<T>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }

    public static PagedResponse<T> Ok(PaginationResponseDto<T> model)
    {
        return new PagedResponse<T>
        {
            Success = true,
            Data = model.Data,
            PageNumber = model.PageNumber,
            PageSize = model.PageSize,
            TotalItems = model.Count,
            TotalPages = (int)Math.Ceiling(model.Count / (double)model.PageSize)
        };
    }

    public static implicit operator PagedResponse<T>(PaginationResponseDto<T> model)
        => Ok(model);
}
```


1. Pagination Properties
   - `PageNumber`, `PageSize`, `TotalPages`, and `TotalItems` provide a comprehensive overview of the pagination state. These properties are calculated based on the total items and page size, making it easier to handle pagination logic.

2. Static Methods
   - The `Ok` method allows creating a successful paginated response based on a `PaginationResponseDto<T>` model, including the paginated data and relevant pagination details.
  
3. Implicit Conversion
   - Implicit conversion from `PaginationResponseDto<T>` to `PagedResponse<T>` is supported, allowing for straightforward and intuitive usage.

## Conclusion

In conclusion, the `BaseResult`, `BaseResult<TData>`, and `PagedResponse<T>` classes, along with the Error class and ErrorCode enumeration, provide a comprehensive framework for handling operation outcomes and reporting errors consistently. By adopting these practices, developers can enhance the reliability and maintainability of their applications while promoting a standardized approach to error management.


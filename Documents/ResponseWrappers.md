# [ASP Dotnet Core Clean Architecture](../README.md) - Response Wrappers

## Introduction
In software development, robust error handling and consistent result communication are essential for building reliable and maintainable applications. This article explores the design and implementation of a result wrapper in C# to streamline the handling of operation outcomes and error reporting.

## The BaseResult Class

The 'BaseResult' class serves as the foundation for handling the results of operations. It includes the following key components:

``` c#
public class BaseResult
{
    public BaseResult()
    {
        Success = true;
        Errors = null;
    }
    public BaseResult(Error error)
    {
        Errors ??= new List<Error>();
        Errors.Add(error);
        Success = false;
    }
    public BaseResult(IEnumerable<Error> errors)
    {
        Errors ??= new List<Error>();
        Errors.AddRange(errors);
        Success = false;
    }
    public bool Success { get; set; }
    public List<Error> Errors { get; set; }
}
```
1. Success Property
    * A boolean property, Success, is employed to indicate whether the operation was successful (true) or encountered an issue (false).

2. Errors Property 
    * The Errors property is a list of Error objects that capture details about any errors that occurred during the operation. This standardized approach allows for consistent error reporting.

3. Constructors
    * The class provides various constructors to handle different scenarios
        1. A default constructor initializes a successful result.
        ``` c#
            public BaseResult()
            {
                Success = true;
                Errors = null;
            }
        ```
        2. Constructors accepting a single Error object or multiple Error objects enable effective error reporting
        ``` c#
            public BaseResult(Error error)
            {
                Errors ??= new List<Error>();
                Errors.Add(error);
                Success = false;
            }
            public BaseResult(IEnumerable<Error> errors)
            {
                Errors ??= new List<Error>();
                Errors.AddRange(errors);
                Success = false;
            }
        ```

## The BaseResult<TData> Generic Class

Building on the foundation of 'BaseResult', the 'BaseResult<TData>' class introduces generics to handle data associated with successful operations. Key elements include:
``` c#
public class BaseResult<TData> : BaseResult
{
    public BaseResult()
    {
    }
    public BaseResult(TData data)
    {
        Success = true;
        Data = data;
        Errors = null;
    }
    public BaseResult(Error error) : base(error)
    {
    }
    public BaseResult(IEnumerable<Error> errors) : bas(errors)
    {
    }
    public TData Data { get; set; }
}
```
1. Data Property
    * The Data property, of type TData, stores the actual data produced by a successful operation. This allows for flexible handling of different data types.

2. Constructors
    * Similar to the base class, constructors are provided for handling successful operations with or without associated data, as well as for reporting errors.

## The Error Class

The 'Error' class represents an individual error and contains the following properties

``` c#
public class Error
{
    public Error(ErrorCode errorCode, string description = null, string fieldName = null)
    {
        ErrorCode = errorCode;
        Description = description;
        FieldName = fieldName;
    }

    public ErrorCode ErrorCode { get; set; }
    public string FieldName { get; set; }
    public string Description { get; set; }
}
```

1. ErrorCode Property
    * An enumeration, 'ErrorCode', categorizes errors into predefined types. This enables developers to identify and handle specific error scenarios systematically.

2. FieldName and Description Properties
    * These properties allow for additional context about the error, specifying the field or area where the error occurred and providing a human-readable description.

## The ErrorCode Enumeration
The 'ErrorCode' enumeration defines a set of error codes that cover various error scenarios, providing a standardized approach to categorizing and handling errors.

```c#
public enum ErrorCode : short
{
    ModelStateNotValid = 0,
    ModelInvariantInvalid = 1,
    FieldDataInvalid = 2,
    MandatoryField = 3,
    InconsistentData = 4,
    RedundantData = 5,
    EmptyData = 6,
    LongData = 7,
    ShortData = 8,
    DataLengthInvalid = 9,
    BirthdateIsAfterNow = 10,
    RequestedDataNotExist = 11,
    DuplicateData = 12,
    DatabaseCommitException = 13,
    DatabaseCommitNotAffected = 14,
    NotFound = 15,
    ModelIsNull = 16,
    NotHaveAnyChangeInData = 17,
    InvalidOperation = 18,
    ThisDataAlreadyExist = 19,
    TamperedData = 20,
    NotInRange = 21,
    ErrorInApiIdentity = 22,
    AccessDenied = 23,
    ErrorInIdentity = 24,
    Exception = 25,
    LicenseException = 26,
}
```

## Conclusion
In conclusion, the use of result wrappers and effective error handling is crucial for building resilient and maintainable C# applications. The presented 'BaseResult' and 'BaseResult<TData>' classes, along with the 'Error' class and 'ErrorCode' enumeration, provide a robust framework for handling operation outcomes and reporting errors consistently. By adopting these practices, developers can enhance the reliability and maintainability of their applications while promoting a standardized approach to error management.

## The PagedResponse Class

The 'PagedResponse<T>' class serves as an extension of the 'BaseResult<List<T>>', introducing properties specifically tailored for paginated responses:

``` c#
public class PagedResponse<T> : BaseResult<List<T>>
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    public PagedResponse()
    {
    }
    public PagedResponse(Error error) : base(error)
    {
    }
    public PagedResponse(PagenationResponseDto<T> model, PagenationRequestParameter request)
    {
        PageNumber = request.PageNumber;
        PageSize = request.PageSize;
        TotalItems = model.Count;
        TotalPages = TotalItems / PageSize;
        if (TotalItems % PageSize > 0) TotalPages++;
        this.Data = model.Data;
        this.Success = true;
    }
    public PagedResponse(PagenationResponseDto<T> model, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = model.Count;
        TotalPages = TotalItems / PageSize;
        if (TotalItems % PageSize > 0) TotalPages++;
        this.Data = model.Data;
        this.Success = true;
    }
}
```

1. Pagination Properties
    * 'PageNumber', 'PageSize', 'TotalPages', and 'TotalItems' provide a comprehensive overview of the pagination state.

    * 'HasPreviousPage' and 'HasNextPage' properties offer convenient checks for navigating between pages.

2. Constructors

    * The default constructor initializes the class.
    * A constructor handling errors is available for scenarios where an operation encounters issues.
    * Constructors based on response models, coupled with pagination parameters or specific page number and size, offer flexibility in constructing responses.


### Utilizing Generics in Pagination

The use of C# generics in the 'PagedResponse<T>' class enables the handling of diverse data types, making it a versatile solution for paginated responses. The generic type 'T' represents the data type of the elements in the response list.

### Examples of Usage

1. Basic Usage
    ``` c#
    var response = new PagedResponse<int>();
    // Initializes a basic paginated response for integers
    ```

1. Handling Errors
    ``` c#
    var errorResponse = new PagedResponse<int>(new Error    (ErrorCode.NotFound, "Requested page not found"));
    // Constructs a paginated response with a specified error
    ```

1. Handling Successful Paginated Responses
    ``` c#
    var paginatedModel = GetPagedData(); // Assume a method to  retrieve paginated data
    var paginationRequest = new PagenationRequestParameter  (pageNumber: 2, pageSize: 10);

    var successResponse = new PagedResponse<int>(paginatedModel,    paginationRequest);
    // Creates a paginated response with success status,    including the paginated data and relevant pagination details
    ```

## Conclusion
In conclusion, the PagedResponse<T> class, built upon the foundations of the BaseResult wrapper and enriched with C# generics, provides a comprehensive solution for handling paginated responses in C# applications. By employing this approach, developers can streamline result communication, manage errors effectively, and seamlessly navigate paginated data. The utilization of generics adds a layer of flexibility, making the solution adaptable to various data types and scenarios, contributing to the overall maintainability and scalability of C# applications.
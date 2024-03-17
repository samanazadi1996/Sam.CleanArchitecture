# [ASP Dotnet Core Clean Architecture](../README.md) - Exception Handling

## Introduction

Effective error management is a crucial aspect of software development, ensuring smooth user experiences and system stability. ASP.NET Core applications are no exception. Utilizing custom middleware for error handling enables developers to have greater control over how their applications behave in the face of errors, providing appropriate responses to users. In this article, we'll delve into the concept of error management in ASP.NET Core applications and implement a custom middleware for handling errors.


## Implementing

To implement error management in ASP.NET Core applications, we'll utilize custom middleware. This middleware, named ErrorHandlerMiddleware, will handle the process of managing errors and sending appropriate responses to users.

``` c#
public class ErrorHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new BaseResult<string>(new Error(ErrorCode.Exception, error?.Message));

            switch (error)
            {
                case ValidationException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Errors = e.Errors.Select(p => new Error(ErrorCode.ModelStateNotValid, p.ErrorMessage, p.PropertyName)).ToList();
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}
```

Subsequently, this middleware is added to the list of ASP.NET Core application middlewares to be automatically invoked during runtime, taking over the responsibility of error management.

``` c# 
app.UseMiddleware<ErrorHandlerMiddleware>();
```

## Advantages

Utilizing custom middleware for error management in ASP.NET Core applications provides several advantages, including:

1. Greater Control: Custom middleware offers more control over the behavior of the application when encountering errors, allowing for tailored responses to users.

2. Flexibility: Custom middleware provides greater flexibility for configuration and customization, enabling adaptation to specific needs of the application.

3. Manageable Code: With custom middleware, code becomes more manageable and understandable, leading to more effective error management.


## Conclusion

In conclusion, this article has explored the importance of error management in ASP.NET Core applications and demonstrated how custom middleware can enhance error handling capabilities. The advantages of utilizing custom middleware include greater control, flexibility, and manageable code. It is hoped that this article has provided insights into using middleware for error management in ASP.NET Core applications, contributing to improved user experiences and system stability.
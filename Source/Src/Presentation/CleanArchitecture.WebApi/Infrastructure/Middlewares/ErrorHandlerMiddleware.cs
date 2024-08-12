using CleanArchitecture.Application.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Infrastructure.Middlewares;

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
            var responseModel = BaseResult.Fail();

            switch (error)
            {
                case ValidationException e:
                    // validation error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.AddErrors(e.Errors.Select(p => new Error(ErrorCode.ModelStateNotValid, p.ErrorMessage, p.PropertyName)).ToList());
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    responseModel.AddError(new Error(ErrorCode.NotFound, error.Message));
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.AddError(new Error(ErrorCode.Exception, error.Message));
                    break;
            }
            var result = JsonSerializer.Serialize(responseModel, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await response.WriteAsync(result);
        }
    }
}

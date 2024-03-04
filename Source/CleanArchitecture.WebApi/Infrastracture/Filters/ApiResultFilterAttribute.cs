using CleanArchitecture.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Net;

namespace CleanArchitecture.WebApi.Infrastracture.Filters
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                var responseModel = new BaseResult();
                responseModel.Success = false;
                foreach (var item in ((ValidationProblemDetails)badRequestObjectResult.Value).Errors)
                {
                    foreach (var val in item.Value)
                    {
                        responseModel.Errors ??= new List<Error>();
                        responseModel.Errors.Add(new Error(ErrorCode.ModelStateNotValid, val, item.Key));
                    }
                }

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(responseModel);
            }
        }
    }
}

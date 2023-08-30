using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TestTemplate.Application.Wrappers;

namespace TestTemplate.WebApi.Infrastracture.Filters
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                BaseResult responseModel = null;
                var errors = ((ValidationProblemDetails)badRequestObjectResult.Value).Errors;

                var errorMessages = errors.Select(p => new { key = p.Key, value = p.Value });
                var temp = new List<Tuple<string, string>>();
                foreach (var item in errorMessages)
                {
                    foreach (var val in item.value)
                    {
                        temp.Add(new Tuple<string, string>(item.key, val));
                    }
                }
                responseModel = new BaseResult(temp.Select(p => new Error(ErrorCode.ModelStateNotValid, p.Item2, p.Item1)));

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(responseModel);
            }
        }
    }
}

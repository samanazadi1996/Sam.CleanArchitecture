using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Presentation.Infrastracture.Extentions;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Infrastracture.Filters
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            string apiVersion = context.GetApiVersions();

            if (context.Result is OkObjectResult okObjectResult)
            {
                var apiResult = new BaseResult(true, "Ok", apiVersion, okObjectResult.Value);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is OkResult okResult)
            {
                var apiResult = new BaseResult(true, "Ok", apiVersion);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is BadRequestResult badRequestResult)
            {
                var apiResult = new BaseResult(true, "BadRequest", apiVersion);
                context.Result = new JsonResult(apiResult);

            }
            else if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {

                var messages = ((Microsoft.AspNetCore.Mvc.ValidationProblemDetails)badRequestObjectResult.Value).Errors;
                List<string> allErrors = new();
                if (badRequestObjectResult.Value is SerializableError errors)
                {
                    allErrors = errors.SelectMany(p => (string[])p.Value).Distinct().ToList();
                }
                else
                {
                    foreach (var item in messages)
                    {
                        foreach (var vals in item.Value)
                        {
                            if (!allErrors.Any())
                                allErrors.Add(vals.ToString());
                        }
                    }
                }

                var apiResult = new BaseResult(false, "BadRequest", apiVersion, null, allErrors);
                context.Result = new JsonResult(apiResult);

            }
            else if (context.Result is ContentResult contentResult)
            {
                var apiResult = new BaseResult(true, "Ok", apiVersion, contentResult.Content);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is NotFoundResult notFoundResult)
            {
                var apiResult = new BaseResult(false, "NotFound", apiVersion);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is NotFoundObjectResult notFoundObjectResult)
            {
                var apiResult = new BaseResult(false, "NotFound", apiVersion, notFoundObjectResult.Value, null);
                context.Result = new JsonResult(apiResult);
                context.HttpContext.Response.StatusCode = notFoundObjectResult.StatusCode.Value;
            }
            else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null && !(objectResult.Value is BaseResult))
            {
                var apiResult = new BaseResult(true, "Ok", apiVersion, objectResult.Value, null);
                context.Result = new JsonResult(apiResult);
            }
            context.HttpContext.Response.StatusCode = context.HttpContext.Response.StatusCode;

            base.OnResultExecuting(context);
        }
    }
}

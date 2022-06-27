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
            var apiVersion = context.GetApiVersions();
            object result = new object();
            if (context.Result is OkObjectResult okObjectResult)
            {
                result = new BaseResult<object>().Ok(okObjectResult.Value);
            }
            else if (context.Result is OkResult okResult)
            {
                result = new JsonResult(new BaseResult());
            }
            else if (context.Result is BadRequestResult badRequestResult)
            {
                result = new BaseResult().BadRequest();
            }
            else if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                var allErrors = new List<string>();
                if (badRequestObjectResult.Value is SerializableError errors)
                {
                    allErrors = errors.SelectMany(p => (string[])p.Value).Distinct().ToList();
                }
                else
                {
                    foreach (var item in ((ValidationProblemDetails)badRequestObjectResult.Value).Errors)
                        foreach (var vals in item.Value)
                            allErrors.Add(vals.ToString());
                }
                result = new BaseResult().BadRequest(allErrors.ToArray());
            }
            else if (context.Result is ContentResult contentResult)
            {
                result = new BaseResult<object>().Ok(contentResult.Content);
            }
            else if (context.Result is NotFoundResult notFoundResult)
            {
                result = new BaseResult().NotFound();
            }
            else if (context.Result is NotFoundObjectResult notFoundObjectResult)
            {
                result = new BaseResult<object>().NotFound(notFoundObjectResult.Value);
            }
            else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null && !(objectResult.Value is BaseResult))
            {
                result = new BaseResult<object>().Ok(objectResult.Value);
            }

            if (context.Result is ObjectResult myObjectResult && myObjectResult.Value is BaseResult baseResult)
                result = baseResult.SetApiVersion(apiVersion);

            context.Result = new JsonResult(result);

            context.HttpContext.Response.StatusCode = context.HttpContext.Response.StatusCode;

            base.OnResultExecuting(context);
        }
    }
}

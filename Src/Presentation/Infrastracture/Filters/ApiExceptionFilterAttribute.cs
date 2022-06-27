using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Presentation.Infrastracture.Extentions;
using System.Net;
using System.Threading.Tasks;

namespace Presentation.Infrastracture.Filters
{

    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var result = new BaseExceptionResult().Exception(context.Exception.Message);
            result.SetApiVersion(context.GetApiVersions());
            var json = JsonConvert.SerializeObject(result);

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.HttpContext.Response.WriteAsync(json);
            context.Result = result;
            await context.Result.ExecuteResultAsync(context);
            await base.OnExceptionAsync(context);
        }
    }
}

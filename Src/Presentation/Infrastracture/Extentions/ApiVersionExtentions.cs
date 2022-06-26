using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Reflection;

namespace Presentation.Infrastracture.Extentions
{
    public static class ApiVersionExtentions
    {
        public static string GetApiVersions(this ExceptionContext context)
        {
            var controller = Type.GetType(((ControllerActionDescriptor)context.ActionDescriptor).ControllerTypeInfo.FullName);
            return string.Join(" - ", controller.GetCustomAttributes<ApiVersionAttribute>()?.SelectMany(p => p.Versions).Select(p => p.MajorVersion + "." + p.MinorVersion));
        }
        public static string GetApiVersions(this ResultExecutingContext context)
        {
            var controller = Type.GetType(context.Controller.ToString());
            return string.Join(" - ", controller.GetCustomAttributes<ApiVersionAttribute>()?.SelectMany(p => p.Versions).Select(p => p.MajorVersion + "." + p.MinorVersion));
        }
    }
}

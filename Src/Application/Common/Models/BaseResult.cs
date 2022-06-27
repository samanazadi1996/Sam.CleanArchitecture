using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class BaseResult : IActionResult
    {
        public BaseResult(bool success, string message, string apiVersion, object result = null, List<string> error = null)
        {
            Success = success;
            Message = message;
            ApiVersion = apiVersion;
            Error = error;
            Result = result;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ApiVersion { get; set; }
        public List<string> Error { get; set; }
        public object Result { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return Task.CompletedTask;
        }
    }
}

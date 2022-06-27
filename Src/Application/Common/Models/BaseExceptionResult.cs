using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class BaseExceptionResult : BaseResult, IActionResult
    {
        public BaseExceptionResult Exception(params string[] errors)
        {
            Success = false;
            Message = "Exception";
            Error = errors.ToList();
            return this;
        }
        public Task ExecuteResultAsync(ActionContext context)
        {
            return Task.CompletedTask;
        }
    }
}

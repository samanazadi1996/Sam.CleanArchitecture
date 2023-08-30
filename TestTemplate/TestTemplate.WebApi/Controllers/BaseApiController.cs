using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TestTemplate.WebApi.Infrastracture.Filters;

namespace TestTemplate.WebApi.Controllers
{
    [ApiController]
    [ApiResultFilter]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}

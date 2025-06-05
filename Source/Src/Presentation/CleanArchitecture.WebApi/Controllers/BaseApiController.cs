using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.WebApi.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.WebApi.Controllers;

[ApiController]
[ApiResultFilter]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public abstract class BaseApiController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}

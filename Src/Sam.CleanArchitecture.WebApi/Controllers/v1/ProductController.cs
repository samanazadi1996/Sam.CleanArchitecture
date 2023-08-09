using Microsoft.AspNetCore.Mvc;
using Sam.CleanArchitecture.Application.Features.Products.Queries.GetById;
using Sam.CleanArchitecture.Application.Features.Products.Queries.GetPagedList;
using Sam.CleanArchitecture.Application.Wrappers;
using Sam.CleanArchitecture.Domain.Products.Dtos;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class ProductController : BaseApiController
    {

        [HttpGet]
        public async Task<PagedResponse<ProductDto>> GetPagedList([FromQuery] GetPagedListQuery model)
            => await Mediator.Send(model);

        [HttpGet]
        public async Task<BaseResult<ProductDto>> GetById([FromQuery] GetByIdQuery model)
            => await Mediator.Send(model);

    }
}
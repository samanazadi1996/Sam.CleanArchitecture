using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestTemplate.Application.Features.Products.Commands.CreateProduct;
using TestTemplate.Application.Features.Products.Commands.DeleteProduct;
using TestTemplate.Application.Features.Products.Commands.UpdateProduct;
using TestTemplate.Application.Features.Products.Queries.GetPagedListProduct;
using TestTemplate.Application.Features.Products.Queries.GetProductById;
using TestTemplate.Application.Wrappers;
using TestTemplate.Domain.Products.Dtos;

namespace TestTemplate.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class ProductController : BaseApiController
    {

        [HttpGet]
        public async Task<PagedResponse<ProductDto>> GetPagedListProduct([FromQuery] GetPagedListProductQuery model)
            => await Mediator.Send(model);

        [HttpGet]
        public async Task<BaseResult<ProductDto>> GetProductById([FromQuery] GetProductByIdQuery model)
            => await Mediator.Send(model);

        [HttpPost]
        public async Task<BaseResult<long>> CreateProduct(CreateProductCommand model)
            => await Mediator.Send(model);

        [HttpPut]
        public async Task<BaseResult> UpdateProduct(UpdateProductCommand model)
            => await Mediator.Send(model);

        [HttpDelete]
        public async Task<BaseResult> DeleteProduct([FromQuery] DeleteProductCommand model)
            => await Mediator.Send(model);

    }
}
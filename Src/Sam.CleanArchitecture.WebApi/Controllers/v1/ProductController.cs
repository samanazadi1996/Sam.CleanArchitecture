using Microsoft.AspNetCore.Mvc;
using Sam.CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using Sam.CleanArchitecture.Application.Features.Products.Commands.DeleteProduct;
using Sam.CleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using Sam.CleanArchitecture.Application.Features.Products.Queries.GetPagedListProduct;
using Sam.CleanArchitecture.Application.Features.Products.Queries.GetProductById;
using Sam.CleanArchitecture.Application.Wrappers;
using Sam.CleanArchitecture.Domain.Products.Dtos;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.WebApi.Controllers.v1
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
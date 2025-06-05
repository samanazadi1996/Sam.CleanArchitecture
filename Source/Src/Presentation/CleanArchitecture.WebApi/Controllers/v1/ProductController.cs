using Asp.Versioning;
using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Commands.DeleteProduct;
using CleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using CleanArchitecture.Application.Features.Products.Queries.GetPagedListProduct;
using CleanArchitecture.Application.Features.Products.Queries.GetProductById;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Domain.Products.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1;

[ApiVersion("1")]
public class ProductController : BaseApiController
{

    [HttpGet]
    public async Task<PagedResponse<ProductDto>> GetPagedListProduct([FromQuery] GetPagedListProductQuery model)
        => await Mediator.Send<GetPagedListProductQuery, PagedResponse<ProductDto>>(model);

    [HttpGet]
    public async Task<BaseResult<ProductDto>> GetProductById([FromQuery] GetProductByIdQuery model)
        => await Mediator.Send<GetProductByIdQuery, BaseResult<ProductDto>>(model);

    [HttpPost, Authorize]
    public async Task<BaseResult<long>> CreateProduct(CreateProductCommand model)
        => await Mediator.Send<CreateProductCommand, BaseResult<long>>(model);

    [HttpPut, Authorize]
    public async Task<BaseResult> UpdateProduct(UpdateProductCommand model)
        => await Mediator.Send<UpdateProductCommand, BaseResult>(model);

    [HttpDelete, Authorize]
    public async Task<BaseResult> DeleteProduct([FromQuery] DeleteProductCommand model)
        => await Mediator.Send<DeleteProductCommand, BaseResult>(model);

}

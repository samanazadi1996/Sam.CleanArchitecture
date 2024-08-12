using AppAny.HotChocolate.FluentValidation;
using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Commands.DeleteProduct;
using CleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using CleanArchitecture.Application.Wrappers;
using HotChocolate.Authorization;
using MediatR;

namespace CleanArchitecture.Infrastructure.GraphQL;

public class Mutation
{

    [Authorize]
    public async Task<BaseResult<long>> CreateProduct([UseFluentValidation] CreateProductCommand model, IMediator mediator)
        => await mediator.Send(model);

    [Authorize]
    public async Task<BaseResult> UpdateProduct([UseFluentValidation] UpdateProductCommand model, IMediator mediator)
        => await mediator.Send(model);

    [Authorize]
    public async Task<BaseResult> DeleteProduct([UseFluentValidation] DeleteProductCommand model, IMediator mediator)
        => await mediator.Send(model);

}
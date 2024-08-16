using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Commands.DeleteProduct;
using CleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using CleanArchitecture.Application.Features.Products.Queries.GetPagedListProduct;
using CleanArchitecture.Application.Features.Products.Queries.GetProductById;
using CleanArchitecture.Infrastructure.GRPC.Common;
using CleanArchitecture.Infrastructure.GRPC.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Infrastructure.GRPC.Services;

public class ProductServiceImpl(IMediator mediator) : ProductService.ProductServiceBase
{
    public override async Task<GetProductByIdResponse> GetProductById(GrpcBaseRequestWithIdParameter request, ServerCallContext context)
    {
        var result = await mediator.Send(new GetProductByIdQuery
        {
            Id = request.Id
        });

        var response = new GetProductByIdResponse
        {
            Success = result.Success,
            Errors = { result.Errors.ToProtobufErrors() }
        };

        if (result is { Success: true, Data: not null })
        {
            response.Data = new ProductModel
            {
                Name = result.Data.Name,
                Price = result.Data.Price,
                BarCode = result.Data.BarCode,
                CreatedDateTime = result.Data.CreatedDateTime.ToLongDateString()
            };
        }

        return response;
    }

    public override async Task<GetPagedListProductResponse> GetPagedListProduct(GetPagedListProductRequest request, ServerCallContext context)
    {
        var result = await mediator.Send(new GetPagedListProductQuery
        {
            Name = request.Name,
            PageSize = request.PageSize,
            PageNumber = request.PageNumber
        });

        var response = new GetPagedListProductResponse
        {
            Success = result.Success,
            Errors = { result.Errors.ToProtobufErrors() },
            PageSize = result.PageSize,
            PageNumber = result.PageNumber,
            TotalItems = result.TotalItems,
            TotalPages = result.TotalPages,
        };

        if (result is { Success: true, Data: not null })
        {
            var data = result.Data?.Select(p => new ProductModel()
            {
                Name = p.Name,
                Price = p.Price,
                BarCode = p.BarCode,
                CreatedDateTime = p.CreatedDateTime.ToLongDateString()
            }).ToList() ?? [];

            response.Data.AddRange(data);
        }

        return response;
    }

    [Authorize]
    public override async Task<GrpcBaseResultWithIntData> CreateProduct(CreateProductRequest request, ServerCallContext context)
    {
        var result = await mediator.Send(new CreateProductCommand()
        {
            Name = request.Name,
            Price = request.Price,
            BarCode =request.BarCode
        });

        return result.ToGrpcBaseResultWithIntData();
    }

    [Authorize]
    public override async Task<GrpcBaseResult> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
    {
        var result = await mediator.Send(new UpdateProductCommand()
        {
            Id = request.Id,
            Name = request.Name,
            Price = request.Price,
            BarCode = request.BarCode
        });

        return result.ToGrpcBaseResult();
    }
    
    [Authorize]
    public override async Task<GrpcBaseResult> DeleteProduct(GrpcBaseRequestWithIdParameter request, ServerCallContext context)
    {
        var result = await mediator.Send(new DeleteProductCommand()
        {
            Id = request.Id,
        });

        return result.ToGrpcBaseResult();
    }
}

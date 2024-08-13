using CleanArchitecture.Application.Features.Products.Queries.GetPagedListProduct;
using CleanArchitecture.Application.Features.Products.Queries.GetProductById;
using CleanArchitecture.Infrastructure.GRPC.Common;
using CleanArchitecture.Infrastructure.GRPC.Protos;
using Grpc.Core;
using MediatR;

namespace CleanArchitecture.Infrastructure.GRPC.Services;

public class ProductServiceImpl(IMediator mediator) : ProductService.ProductServiceBase
{
    public override async Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request, ServerCallContext context)
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
}

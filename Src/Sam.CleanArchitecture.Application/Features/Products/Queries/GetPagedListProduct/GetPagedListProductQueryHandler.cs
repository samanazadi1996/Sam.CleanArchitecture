using MediatR;
using Sam.CleanArchitecture.Application.Interfaces.Repositories;
using Sam.CleanArchitecture.Application.Wrappers;
using Sam.CleanArchitecture.Domain.Products.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.Application.Features.Products.Queries.GetPagedListProduct
{
    public class GetPagedListProductQueryHandler : IRequestHandler<GetPagedListProductQuery, PagedResponse<ProductDto>>
    {
        private readonly IProductRepository productRepository;

        public GetPagedListProductQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<PagedResponse<ProductDto>> Handle(GetPagedListProductQuery request, CancellationToken cancellationToken)
        {
            var result = await productRepository.GetPagedListAsync(request.PageNumber, request.PageSize, request.Name);

            return new PagedResponse<ProductDto>(result, request);
        }
    }
}

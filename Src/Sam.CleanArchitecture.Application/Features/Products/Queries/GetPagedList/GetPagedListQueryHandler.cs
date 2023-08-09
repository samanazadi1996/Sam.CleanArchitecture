using MediatR;
using Sam.CleanArchitecture.Application.Interfaces.Repositories;
using Sam.CleanArchitecture.Application.Wrappers;
using Sam.CleanArchitecture.Domain.Products.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.Application.Features.Products.Queries.GetPagedList
{
    public class GetPagedListQueryHandler : IRequestHandler<GetPagedListQuery, PagedResponse<ProductDto>>
    {
        private readonly IProductRepository productRepository;

        public GetPagedListQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<PagedResponse<ProductDto>> Handle(GetPagedListQuery request, CancellationToken cancellationToken)
        {
            var result = await productRepository.GetPagedListAsync(request.PageNumber, request.PageSize);

            return new PagedResponse<ProductDto>(result, request);
        }
    }
}

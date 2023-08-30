using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestTemplate.Application.Interfaces.Repositories;
using TestTemplate.Application.Wrappers;
using TestTemplate.Domain.Products.Dtos;

namespace TestTemplate.Application.Features.Products.Queries.GetPagedListProduct
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

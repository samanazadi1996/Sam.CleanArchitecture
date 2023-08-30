using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestTemplate.Application.Interfaces;
using TestTemplate.Application.Interfaces.Repositories;
using TestTemplate.Application.Wrappers;
using TestTemplate.Domain.Products.Dtos;

namespace TestTemplate.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, BaseResult<ProductDto>>
    {
        private readonly IProductRepository productRepository;
        private readonly ITranslator translator;

        public GetProductByIdQueryHandler(IProductRepository productRepository, ITranslator translator)
        {
            this.productRepository = productRepository;
            this.translator = translator;
        }

        public async Task<BaseResult<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id);

            if (product is null)
            {
                return new BaseResult<ProductDto>(new Error(ErrorCode.NotFound, translator.GetString(Messages.ProductMessages.Product_notfound_with_id(request.Id)), nameof(request.Id)));
            }

            var result = new ProductDto(product);

            return new BaseResult<ProductDto>(result);
        }
    }
}

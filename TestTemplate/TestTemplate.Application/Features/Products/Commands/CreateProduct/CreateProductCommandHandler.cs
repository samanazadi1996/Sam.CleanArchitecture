using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestTemplate.Application.Interfaces;
using TestTemplate.Application.Interfaces.Repositories;
using TestTemplate.Application.Wrappers;
using TestTemplate.Domain.Products.Entities;

namespace TestTemplate.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseResult<long>>
    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<BaseResult<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Price, request.BarCode);

            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();

            return new BaseResult<long>(product.Id);
        }
    }
}

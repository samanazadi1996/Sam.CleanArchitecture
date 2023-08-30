using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestTemplate.Application.Interfaces;
using TestTemplate.Application.Interfaces.Repositories;
using TestTemplate.Application.Wrappers;

namespace TestTemplate.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseResult>
    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITranslator translator;

        public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ITranslator translator)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
            this.translator = translator;
        }

        public async Task<BaseResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id);

            if (product is null)
            {
                return new BaseResult(new Error(ErrorCode.NotFound, translator.GetString(Messages.ProductMessages.Product_notfound_with_id(request.Id)), nameof(request.Id)));
            }

            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();

            return new BaseResult();
        }
    }
}

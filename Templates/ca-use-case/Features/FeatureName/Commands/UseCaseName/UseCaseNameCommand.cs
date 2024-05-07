using CleanArchitecture.Application.Wrappers;
using MediatR;

namespace CleanArchitecture.Application.Features.FeatureName.Commands.UseCaseName
{
    public class UseCaseNameCommand : IRequest<BaseResult<object>>
    {
        public object MyProperty { get; set; }
    }
}
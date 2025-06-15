using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Wrappers;

namespace CleanArchitecture.Application.Features.FeatureName.Commands.UseCaseName
{
    public class UseCaseNameCommand : IRequest<BaseResult<object>>
    {
        public object MyProperty { get; set; }
    }
}
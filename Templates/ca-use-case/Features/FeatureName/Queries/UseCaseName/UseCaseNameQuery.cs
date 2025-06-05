using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Wrappers;

namespace CleanArchitecture.Application.Features.FeatureName.Queries.UseCaseName
{
    public class UseCaseNameQuery : IRequest<BaseResult<object>>
    {
        public object MyProperty { get; set; }
    }
}

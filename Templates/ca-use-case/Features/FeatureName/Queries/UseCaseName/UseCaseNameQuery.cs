using CleanArchitecture.Application.Wrappers;
using MediatR;

namespace CleanArchitecture.Application.Features.FeatureName.Queries.UseCaseName
{
    public class UseCaseNameQuery : IRequest<BaseResult<object>>
    {
        public object MyProperty { get; set; }
    }
}

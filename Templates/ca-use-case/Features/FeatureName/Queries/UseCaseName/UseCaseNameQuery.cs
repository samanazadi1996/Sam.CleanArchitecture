using CleanArchitecture.Application.Wrappers;
using MediatR;

namespace CleanArchitecture.Application.Features.FeatureName.Queries.UseCaseName
{
    public class UseCaseNameQuery : IRequest<BaseResult<string>>
    {
        public string Str { get; set; }
    }
}

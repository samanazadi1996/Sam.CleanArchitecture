using CleanArchitecture.Application.Wrappers;
using MediatR;

namespace CleanArchitecture.Application.Features.DomainName.Queries.UseCaseName
{
    public class UseCaseNameQuery : IRequest<BaseResult<string>>
    {
        public string Str { get; set; }
    }
}

using CleanArchitecture.Application.Wrappers;
using MediatR;

namespace CleanArchitecture.Application.Features.DomainName.Commands.UseCaseName
{
    public class UseCaseNameCommand : IRequest<BaseResult<long>>
    {
        // public string Name { get; set; }
    }
}
using CleanArchitecture.Application.Wrappers;
using MediatR;

namespace CleanArchitecture.Application.Features.FeatureName.Commands.UseCaseName
{
    public class UseCaseNameCommand : IRequest<BaseResult<long>>
    {
        // public string Name { get; set; }
    }
}
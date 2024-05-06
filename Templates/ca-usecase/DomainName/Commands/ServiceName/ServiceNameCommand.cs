using CleanArchitecture.Application.Wrappers;
using MediatR;

namespace CleanArchitecture.Application.Features.Products.Commands.ServiceName
{
    public class ServiceNameCommand : IRequest<BaseResult<long>>
    {
        // public string Name { get; set; }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces;

public interface IMediator
{
    Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>;
}

public interface IRequest<TResponse>
{

}
public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}

public interface IPipelineBehavior<in TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken = default);
}
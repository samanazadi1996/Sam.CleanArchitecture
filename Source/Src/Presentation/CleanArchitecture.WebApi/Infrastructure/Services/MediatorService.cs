using CleanArchitecture.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Infrastructure.Services;

public class MediatorService(IServiceProvider serviceProvider) : IMediator
{
    public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
    {

        var handler = serviceProvider.GetService<IRequestHandler<TRequest, TResponse>>();

        if (handler == null)
            throw new InvalidOperationException($"Handler not found for request type {request.GetType()}");

        var behaviors = serviceProvider.GetServices<IPipelineBehavior<TRequest, TResponse>>().Reverse();
        Func<Task<TResponse>> handlerDelegate = () => handler.HandleAsync(request, cancellationToken);
        foreach (var behavior in behaviors)
        {
            var next = handlerDelegate;
            handlerDelegate = () => behavior.HandleAsync(request, next, cancellationToken);
        }

        return await handlerDelegate();

    }
}

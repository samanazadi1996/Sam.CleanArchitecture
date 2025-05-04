using CleanArchitecture.Application.Interfaces;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.WebApi.Infrastructure.Services;

public class MediatorService(IServiceProvider serviceProvider) : IMediator
{
    public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
    {

        var handler = serviceProvider.GetService<IRequestHandler<TRequest, TResponse>>();

        if (handler == null)
            throw new InvalidOperationException($"Handler not found for request type {request.GetType()}");

        return await handler.HandleAsync(request, cancellationToken);

    }
}

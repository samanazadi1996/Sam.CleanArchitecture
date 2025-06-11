using CleanArchitecture.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Infrastructure.Services;

public class MediatorService(IServiceProvider serviceProvider) : IMediator
{
    private static readonly MethodInfo GenericSendMethod = typeof(MediatorService)
        .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        .First(m => m.Name == nameof(Send) && m.IsGenericMethodDefinition && m.GetGenericArguments().Length == 2);

    private static readonly ConcurrentDictionary<Type, MethodInfo> SendMethodCache = new();

    public async Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var handler = serviceProvider.GetService<IRequestHandler<TRequest, TResponse>>();
        if (handler is null)
            throw new InvalidOperationException($"No handler found for request of type {typeof(TRequest).FullName}");

        var behaviors = serviceProvider.GetServices<IPipelineBehavior<TRequest, TResponse>>().Reverse().ToArray();

        Func<Task<TResponse>> handlerDelegate = () => handler.Handle(request, cancellationToken);

        foreach (var behavior in behaviors)
        {
            var next = handlerDelegate;
            handlerDelegate = () => behavior.Handle(request, next, cancellationToken);
        }

        return await handlerDelegate().ConfigureAwait(false);
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var requestType = request.GetType();

        var method = SendMethodCache.GetOrAdd(requestType, type =>
            GenericSendMethod.MakeGenericMethod(type, typeof(TResponse)));

        var task = (Task<TResponse>)method.Invoke(this, [request, cancellationToken])!;
        return await task.ConfigureAwait(false);
    }
}

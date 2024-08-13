using CleanArchitecture.Infrastructure.GRPC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.GRPC;

public static class ServiceRegistration
{
    public static IServiceCollection AddGrpcInfrastructure(this IServiceCollection services)
    {
        services.AddGrpc();

        return services;
    }

    public static IEndpointRouteBuilder MapGrpcServices(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGrpcService<ProductServiceImpl>();

        return endpoints; 
    }

}

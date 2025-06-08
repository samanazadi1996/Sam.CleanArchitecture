using AppAny.HotChocolate.FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.GraphQL;
public static class ServiceRegistration
{
    public static IServiceCollection AddGraphQlInfrastructure(this IServiceCollection services)
    {
        services.AddGraphQLServer()
            .AddFluentValidation()
            .AddTypes(typeof(Query), typeof(Mutation))
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddQueryableCursorPagingProvider()
            .AddAuthorization()
            .InitializeOnStartup();

        return services;
    }
}
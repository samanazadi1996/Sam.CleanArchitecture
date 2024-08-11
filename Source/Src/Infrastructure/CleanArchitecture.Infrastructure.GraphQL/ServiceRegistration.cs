using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infrastructure.GraphQL.Behaviors;
using CleanArchitecture.Infrastructure.Identity.Contexts;
using CleanArchitecture.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.GraphQL;
public static class ServiceRegistration
{
    public static IServiceCollection AddGraphQlInfrastructure(this IServiceCollection services)
    {
        services.AddGraphQLServer()
            .AddTypes(typeof(Query))
            .RegisterDbContext<ApplicationDbContext>()
            .RegisterDbContext<IdentityContext>()
            .RegisterService<IAuthenticatedUserService>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddQueryableCursorPagingProvider()
            .AddAuthorizationHandler<AuthorizationHandler>()
            .InitializeOnStartup();

        return services;
    }
}
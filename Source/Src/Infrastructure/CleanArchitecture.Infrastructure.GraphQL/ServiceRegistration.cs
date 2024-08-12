using AppAny.HotChocolate.FluentValidation;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infrastructure.Identity.Contexts;
using CleanArchitecture.Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.GraphQL;
public static class ServiceRegistration
{
    public static IServiceCollection AddGraphQlInfrastructure(this IServiceCollection services)
    {
        services.AddGraphQLServer()
            .AddFluentValidation()
            .AddTypes(typeof(Query), typeof(Mutation))
            .RegisterDbContext<ApplicationDbContext>()
            .RegisterDbContext<IdentityContext>()
            .RegisterService<IMediator>()
            .RegisterService<IAuthenticatedUserService>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddQueryableCursorPagingProvider()
            .AddAuthorization()
            .InitializeOnStartup();

        return services;
    }
}
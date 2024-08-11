using CleanArchitecture.Application.Interfaces;
using HotChocolate.Authorization;
using HotChocolate.Resolvers;

namespace CleanArchitecture.Infrastructure.GraphQL.Behaviors;

public class AuthorizationHandler(IAuthenticatedUserService authenticatedUserService) : IAuthorizationHandler
{
    public ValueTask<AuthorizeResult> AuthorizeAsync(IMiddlewareContext context, AuthorizeDirective directive, CancellationToken cancellationToken = new CancellationToken())
    {

        if (string.IsNullOrEmpty(authenticatedUserService.UserId))
        {
            return new ValueTask<AuthorizeResult>(AuthorizeResult.NotAllowed);
        }
        return new ValueTask<AuthorizeResult>(AuthorizeResult.Allowed);
    }

    public ValueTask<AuthorizeResult> AuthorizeAsync(AuthorizationContext context, IReadOnlyList<AuthorizeDirective> directives, CancellationToken cancellationToken = new CancellationToken())
    {
        if (string.IsNullOrEmpty(authenticatedUserService.UserId))
        {
            return new ValueTask<AuthorizeResult>(AuthorizeResult.NotAllowed);
        }
        return new ValueTask<AuthorizeResult>(AuthorizeResult.Allowed);
    }
}
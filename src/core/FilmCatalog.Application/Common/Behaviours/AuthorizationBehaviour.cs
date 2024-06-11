using System.Reflection;
using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Common.Security;
using FilmCatalog.Domain.Enums;
using MediatR;

namespace FilmCatalog.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    private readonly IIdentityService _identityService;

    public AuthorizationBehaviour(
        IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            var user = await _identityService.GetCurrentUserAsync();

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            // Role-based authorization
            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => a.Role != Role.None);

            if (authorizeAttributesWithRoles.Any())
            {
                var authorized = false;

                foreach (var role in authorizeAttributesWithRoles.Select(a => a.Role))
                {
                    if (user.Role == role)
                    {
                        authorized = true;
                        break;
                    }
                }

                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}

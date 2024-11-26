using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using static Packages.Resources.Constants;

namespace Packages.Pipelines.Authorization
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IRequireAuthorization authorizationRequest)
            {
                var user = _httpContextAccessor.HttpContext?.User;

                if (user == null || !user.Identity?.IsAuthenticated == true)
                    throw new UnauthorizedAccessException(AuthorizationErrorMessage.UserNotAuthenticated);

                var roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value);

                if (roles.Contains("Admin"))
                    return await next();
                
                if (!roles.Contains(authorizationRequest.RequiredRole))
                    throw new UnauthorizedAccessException(string.Format(AuthorizationErrorMessage.UserNotAuthorized, authorizationRequest.RequiredRole));
            }

            return await next();
        }
    }

}

using CommonLibrary.Features.Paginations;
using CommonLibrary.Resources;
using ManagementSystem.Domain.Models.Args.User;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Models.Enums;
using MediatR;
using Packages.Pipelines.Authorization;
using Packages.Pipelines.Caching;

namespace ManagementSystem.Application.Features.Queries.User
{
    public class GetUsersQuery : GetUserArgs, IRequest<PagedViewModel<UserDto>>, ICachableRequest, IRequireAuthorization
    {
        List<string> IRequireAuthorization.RequiredRole => new List<string> { nameof(Roles.Admin), nameof(Roles.Manager) };
        public string CacheKey => string.Format(Constants.Caches.Users.CacheKey, Page, PageSize);
        public bool BypassCache { get; }
        public TimeSpan? SlidingExpiration { get; }
        public string? CacheGroupKey => Constants.Caches.Users.CachceGroupKey;
    }
}
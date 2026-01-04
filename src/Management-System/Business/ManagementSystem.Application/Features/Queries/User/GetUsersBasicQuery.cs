using CommonLibrary.Resources;
using MediatR;
using Packages.Pipelines.Caching;

namespace ManagementSystem.Application.Features.Queries.User
{
    public class GetUsersBasicQuery : IRequest<List<Domain.Models.Dto.UserDto>>, ICachableRequest
    {
        public string CacheKey => Constants.Caches.Users.BasicCacheKey;
        public bool BypassCache { get; }
        public TimeSpan? SlidingExpiration { get; }
        public string? CacheGroupKey => Constants.Caches.Users.CachceGroupKey;
    }
}

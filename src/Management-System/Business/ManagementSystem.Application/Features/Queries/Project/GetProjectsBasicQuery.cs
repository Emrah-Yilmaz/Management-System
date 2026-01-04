using CommonLibrary.Resources;
using MediatR;
using Packages.Pipelines.Caching;

namespace ManagementSystem.Application.Features.Queries.Project
{
    public class GetProjectsBasicQuery : IRequest<List<ManagementSystem.Domain.Models.Dto.ProjectDto>>, ICachableRequest
    {
        public string CacheKey => Constants.Caches.Projects.BasicCacheKey;
        public bool BypassCache { get; }
        public TimeSpan? SlidingExpiration { get; }
        public string? CacheGroupKey => Constants.Caches.Projects.CachceGroupKey;
    }
}
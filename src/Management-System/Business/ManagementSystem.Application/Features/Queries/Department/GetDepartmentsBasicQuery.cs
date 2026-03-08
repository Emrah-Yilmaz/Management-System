using CommonLibrary.Resources;
using MediatR;
using Packages.Pipelines.Caching;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class GetDepartmentsBasicQuery : IRequest<List<ManagementSystem.Domain.Models.Dto.DepartmentDto>>, ICachableRequest
    {
        public string CacheKey => Constants.Caches.Department.BasicCacheKey;
        public bool BypassCache { get; }
        public TimeSpan? SlidingExpiration { get; }
        public string? CacheGroupKey => Constants.Caches.Department.CachceGroupKey;
    }
}
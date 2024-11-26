using CommonLibrary.Features.Paginations;
using CommonLibrary.Resources;
using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Models.Enums;
using MediatR;
using Packages.Pipelines.Authorization;
using Packages.Pipelines.Caching;
using Packages.Pipelines.Validation;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class GetDeparmentsQuery : GetDepartmentsArgs, IRequest<PagedViewModel<DepartmentDto>>, ICachableRequest, IRequireAuthorization, IRequestValidator
    {
        List<string> IRequireAuthorization.RequiredRole => new List<string> { nameof(Roles.Admin) };
        public string CacheKey => string.Format(Constants.Caches.Department.CacheKey, Page, PageSize);
        public bool BypassCache { get; }
        public TimeSpan? SlidingExpiration { get; }
        public string? CacheGroupKey => Constants.Caches.Department.CachceGroupKey;

    }
}

using CommonLibrary.Models.Args;
using CommonLibrary.Resources;
using MediatR;
using Packages.Pipelines.Caching;

namespace ManagementSystem.Application.Features.Commands.Department.Delete
{
    public class DeleteDepartmentCommand : GetByIdArgs, IRequest<int>, ICacheRemoverRequest
    {
        public string? CacheKey => default;

        public bool BypassCache => default;

        public string? CacheGroupKey => Constants.Caches.Department.CachceGroupKey;
    }
}

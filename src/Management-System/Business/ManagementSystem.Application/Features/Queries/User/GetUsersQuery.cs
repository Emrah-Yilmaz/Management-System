using CommonLibrary.Features.Paginations;
using ManagementSystem.Domain.Models.Args.User;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Models.Enums;
using MediatR;
using Packages.Pipelines.Authorization;

namespace ManagementSystem.Application.Features.Queries.User
{
    public class GetUsersQuery : GetUserArgs, IRequest<PagedViewModel<UserDto>>,  IRequireAuthorization
    {
        List<string> IRequireAuthorization.RequiredRole => new List<string> { nameof(Roles.Admin), nameof(Roles.Manager) };
        public bool BypassCache { get; }
        public TimeSpan? SlidingExpiration { get; }
    }
}
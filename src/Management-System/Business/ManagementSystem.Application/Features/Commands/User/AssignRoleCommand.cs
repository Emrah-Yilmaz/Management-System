using ManagementSystem.Domain.Models.Args.User;
using MediatR;
using Packages.Pipelines.Authorization;

namespace ManagementSystem.Application.Features.Commands.User
{
    public class AssignRoleCommand : AssignRoleArgs, IRequest<bool>, IRequireAuthorization
    {
        public List<string> RequiredRole => new List<string> { nameof(Domain.Models.Enums.Roles.Admin), nameof(Domain.Models.Enums.Roles.Manager) };
    }
}

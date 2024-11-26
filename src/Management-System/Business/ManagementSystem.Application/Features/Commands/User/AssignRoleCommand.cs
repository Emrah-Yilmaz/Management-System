using CommonLibrary.Models.Args;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.User
{
    public class AssignRoleCommand : GetByIdArgs, IRequest<bool>
    {
    }
}

using CommonLibrary.Models.Args;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.User
{
    public class GetUserRolesQuery : GetByIdArgs, IRequest<Domain.Entities.User>
    {
    }
}
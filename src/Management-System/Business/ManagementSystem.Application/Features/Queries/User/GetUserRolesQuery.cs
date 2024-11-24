using CommonLibrary.Models.Args;
using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.User
{
    public class GetUserRolesQuery : GetByIdArgs, IRequest<UserDto>
    {
    }
}
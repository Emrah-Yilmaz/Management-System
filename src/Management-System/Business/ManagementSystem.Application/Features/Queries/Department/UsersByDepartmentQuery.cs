using CommonLibrary.Models.Args;
using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class UsersByDepartmentQuery : GetByIdArgs, IRequest<UsersByDepartmentDto>
    {
    }
}
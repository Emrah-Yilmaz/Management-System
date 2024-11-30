using ManagementSystem.Domain.Models.Args.Project;
using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Project;
public class GetProjectQuery : GetProjectArgs, IRequest<ProjectUsersDto?>
{
}
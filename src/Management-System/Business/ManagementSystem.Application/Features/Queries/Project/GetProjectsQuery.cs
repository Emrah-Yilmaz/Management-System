using CommonLibrary.Features.Paginations;
using ManagementSystem.Domain.Models.Args.Project;
using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Project;
public class GetProjectsQuery : SearchProjectArgs, IRequest<PagedViewModel<ProjectDto>>
{
}
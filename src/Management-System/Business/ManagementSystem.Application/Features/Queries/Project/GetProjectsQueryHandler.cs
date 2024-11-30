using CommonLibrary.Features.Paginations;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.Project;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Project;
public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, PagedViewModel<ProjectDto>>
{
    private readonly IProjectService _projectService;

    public GetProjectsQueryHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public Task<PagedViewModel<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return _projectService.SearchAsync(request, cancellationToken);
    }
}

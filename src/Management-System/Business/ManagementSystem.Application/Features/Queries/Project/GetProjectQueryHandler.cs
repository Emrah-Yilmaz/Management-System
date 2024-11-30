using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.Project;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Project;
public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectUsersDto>
{
    private readonly IProjectService _projectService;

    public GetProjectQueryHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<ProjectUsersDto?> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        return await _projectService.GetProjectAsync(request, cancellationToken);
    }
}
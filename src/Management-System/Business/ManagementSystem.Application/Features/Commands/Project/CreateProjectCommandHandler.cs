using ManagementSystem.Domain.Services.Abstract.Project;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Project;
public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
{
    private readonly IProjectService _projectService;

    public CreateProjectCommandHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        await _projectService.Create(request, cancellationToken);
    }
}

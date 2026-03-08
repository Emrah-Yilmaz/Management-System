using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.Project;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Project
{
    public class GetProjectsBasicQueryHandler : IRequestHandler<GetProjectsBasicQuery, List<ProjectDto>>
    {
        private readonly IProjectService _service;

        public GetProjectsBasicQueryHandler(IProjectService service)
        {
            _service = service;
        }

        public async Task<List<ProjectDto>> Handle(GetProjectsBasicQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetProjectsBasicAsync(cancellationToken);
        }
    }
}
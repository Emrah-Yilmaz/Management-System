using CommonLibrary.Features.Paginations;
using ManagementSystem.Domain.Models.Args.Project;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Services.Abstract.Project;
public interface IProjectService : IDomainService
{
    Task Create(CreateProjectArgs args, CancellationToken cancellationToken = default);
    Task<PagedViewModel<ProjectDto>> SearchAsync(SearchProjectArgs args, CancellationToken cancellationToken = default);
    Task<ProjectUsersDto?> GetProjectAsync(GetProjectArgs args, CancellationToken cancellationToken = default);
}
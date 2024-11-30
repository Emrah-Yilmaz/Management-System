using AutoMapper;
using AutoMapper.QueryableExtensions;
using CommonLibrary.Extensions;
using CommonLibrary.Features.Paginations;
using CommonLibrary.Models.Args;
using ManagementSystem.Domain.Models.Args.Project;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Persistence.Comment;
using ManagementSystem.Domain.Persistence.Department;
using ManagementSystem.Domain.Services.Abstract.Project;
using ManagementSystem.Domain.Utilities;
using Packages.Exceptions.Types;

namespace ManagementSystem.Domain.Services.Concrete;
public class ProjectService : IProjectService
{
    private readonly IProjectRepository _repository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository repository, IMapper mapper, IDepartmentRepository departmentRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _departmentRepository = departmentRepository;
    }

    public async Task Create(CreateProjectArgs args, CancellationToken cancellationToken = default)
    {
        var isExist = await _repository.IsExistAsync(
            predicate: p => p.Name == args.Name,
            cancellationToken: cancellationToken
        );

        if (isExist)
            throw new BusinessException(string.Format(Shared.ErrorMessage.AlreadyExist, Shared.Entities.Project));

        var department = await _departmentRepository.FirstOrDefaultAsync(
            predicate: d => d.Id == args.DepartmentId,
            isDeleted: false,
            noTracking: false,
            cancellationToken: cancellationToken,
            includes: p => p.Projects
        );

        if (department is null)
            throw new BusinessException(string.Format(Shared.Entities.Department, Shared.ErrorMessage.NotFoundError));

        var mappedEntity = _mapper.Map<Domain.Entities.Project>(args);

        department?.Projects?.Add(mappedEntity);

        var saved = await _departmentRepository.SaveChangeAsync(cancellationToken);

        if (saved > 0)
            return;

        throw new BusinessException(Shared.ErrorMessage.SavedError);
    }

    public async Task<ProjectUsersDto?> GetProjectAsync(GetProjectArgs args, CancellationToken cancellationToken = default)
    {
        var project = await _repository.FirstOrDefaultAsync
        (
            predicate: p => p.Id == args.Id,
            isDeleted: false,
            noTracking: true,
            cancellationToken: cancellationToken,
            includes: args.Type == Models.Enums.ProjectRequestType.Basic ? null : u => u.Users
        );
        if (project is null)
            return null;

        var mappedResult = _mapper.Map<ProjectUsersDto>(project);
        return mappedResult;
    }

    public async Task<PagedViewModel<ProjectDto>> SearchAsync(SearchProjectArgs args, CancellationToken cancellationToken = default)
    {
        var query = _repository.AsQueryable(
            expression: args.Name is null ? null : p => p.Name.Contains(args.Name),
            isDeleted: false
        );

        if (query is null)
            return null;

        var projects = await query
            .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
            .GetPaged(args.Page, args.PageSize);

        return projects;
    }
}
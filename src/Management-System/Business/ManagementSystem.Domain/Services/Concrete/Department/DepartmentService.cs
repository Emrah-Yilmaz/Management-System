using AutoMapper;
using AutoMapper.QueryableExtensions;
using CommonLibrary.Extensions;
using CommonLibrary.Features.Paginations;
using CommonLibrary.Models.Args;
using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Persistence.Department;
using ManagementSystem.Domain.Services.Abstract.Department;

namespace ManagementSystem.Domain.Services.Concrete.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;


        public DepartmentService(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateDepartmentArgs args, CancellationToken cancellationToken = default)
        {
            var mappedEntity = _mapper.Map<Domain.Entities.Department>(args);
            var result = await _repository.AddAsync(mappedEntity, cancellationToken);
            return result;
        }

        public async Task<int> Deletesync(GetByIdArgs args, CancellationToken cancellationToken = default)
        {
            var result = await _repository.DeleteAsync(args.Id, cancellationToken);
            return result;
        }

        public async Task<DepartmentDto> GetDepartment(GetByIdArgs args, CancellationToken cancellationToken = default)
        {
            var result = await _repository.SingleOrDefaultAsync(p => p.Id == args.Id, isDeleted: false);
            if (result is null)
            {
                return null;
            }

            var mappedResult = _mapper.Map<DepartmentDto>(result);
            return mappedResult;
        }

        public async Task<PagedViewModel<DepartmentDto>?> GetDepartments(GetDepartmentsArgs args, CancellationToken cancellationToken = default)
        {
            var query = (args.Name is null) ?  _repository.AsQueryable(isDeleted: false) : _repository.AsQueryable(p => p.Name.Contains(args.Name), isDeleted: false);

            var departments = await query
                .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
                .GetPaged(args.Page, args.PageSize);

            if (departments is null || departments.Results.Count == 0)
            {
                return null;
            }

            return departments;
        }

        public async Task<DepartmentDto> GetProjectsByDepartment(GetByIdArgs args, CancellationToken cancellationToken = default)
        {
            var result = await _repository.SingleOrDefaultAsync(
                predicate: p => p.Id == args.Id,
                isDeleted: false,
                noTracking: true,
                cancellationToken: default,
                includes: p => p.Projects);

            if (result is null)
            {
                return null;
            }

            var mappedResult = _mapper.Map<DepartmentDto>(result);
            return mappedResult;
        }

        public async Task<UsersByDepartmentDto> GetUsersByDepartment(GetByIdArgs args, CancellationToken cancellationToken = default)
        {
            var department = _repository.Get(
                predicate: d => d.Id == args.Id,
                noTracking: false,
                includes: u => u.Users).FirstOrDefault();
            var mappedResult = _mapper.Map<UsersByDepartmentDto>(department);
            return mappedResult;
        }

        public async Task<int> UpdateAsync(UpdateDepartmenArgs args, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.FindAsync(args.Id);
            if (entity is null)
            {
                return 0;
            }
            entity.Name = args.Name;
            var result = await _repository.UpdateAsync(entity);
            return result;
        }
    }
}

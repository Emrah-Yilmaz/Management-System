using CommonLibrary.Features.Paginations;
using CommonLibrary.Models.Args;
using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Services.Abstract.Department
{
    public interface IDepartmentService : IDomainService
    {
        public Task<int> CreateAsync(CreateDepartmentArgs args, CancellationToken cancellationToken = default);
        public Task<int> Deletesync(GetByIdArgs args, CancellationToken cancellationToken = default);
        public Task<int> UpdateAsync(UpdateDepartmenArgs args, CancellationToken cancellationToken = default);
        public Task<DepartmentDto> GetDepartment(GetByIdArgs args, CancellationToken cancellationToken = default);
        public Task<DepartmentDto> GetProjectsByDepartment(GetByIdArgs args, CancellationToken cancellationToken = default);
        public Task<UsersByDepartmentDto> GetUsersByDepartment(GetByIdArgs args, CancellationToken cancellationToken = default);
        public Task<PagedViewModel<DepartmentDto>> GetDepartments(GetDepartmentsArgs args, CancellationToken cancellationToken = default);
    }
}

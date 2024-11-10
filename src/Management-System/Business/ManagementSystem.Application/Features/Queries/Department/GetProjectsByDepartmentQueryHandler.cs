using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.Department;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class GetProjectsByDepartmentQueryHandler : IRequestHandler<GetProjectsByDepartmentQuery, DepartmentDto>
    {
        private readonly IDepartmentService _departmentService;

        public GetProjectsByDepartmentQueryHandler(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<DepartmentDto> Handle(GetProjectsByDepartmentQuery request, CancellationToken cancellationToken)
        {
            return await _departmentService.GetProjectsByDepartment(request, cancellationToken);
        }
    }
}

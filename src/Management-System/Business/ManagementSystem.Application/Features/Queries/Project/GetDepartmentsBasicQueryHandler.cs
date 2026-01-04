using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.Department;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Department
{
    public class GetDepartmentsBasicQueryHandler : IRequestHandler<GetDepartmentsBasicQuery, List<DepartmentDto>>
    {
        private readonly IDepartmentService _service;

        public GetDepartmentsBasicQueryHandler(IDepartmentService service)
        {
            _service = service;
        }

        public async Task<List<DepartmentDto>> Handle(GetDepartmentsBasicQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetDepartmentsBasicAsync(cancellationToken);
        }
    }
}
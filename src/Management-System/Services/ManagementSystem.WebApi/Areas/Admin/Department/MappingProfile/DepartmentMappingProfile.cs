using AutoMapper;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Areas.Admin.Department.MappingProfile.Models.Responses;
using ManagementSystem.WebApi.Areas.Admin.Departments.Models.Responses;

namespace ManagementSystem.WebApi.Areas.Admin.Departments.MappingProfile
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<DepartmentDto, DepartmentResponse>().ReverseMap();
            CreateMap<DepartmentDto, GetDepartmentBasicResponse>().ReverseMap();
        }
    }
}
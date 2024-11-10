using CommonLibrary.Features.Paginations;
using CommonLibrary.Models.Args;
using ManagementSystem.Application.Features.Commands.Department.Delete;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Response.Department;
using ManagementSystem.WebApi.Models.Response.Projects;
using ManagementSystem.WebApi.Models.Response.User;

namespace ManagementSystem.WebApi.MappingProfile.Department
{
    public class DepartmentMappingProfile : AutoMapper.Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<PagedViewModel<DepartmentResponse>, PagedViewModel<DepartmentDto>>()
                .ForMember(dest => dest.Results, opt => opt.MapFrom(source => source.Results))
                .ReverseMap();
            CreateMap<DepartmentResponse, DepartmentDto>().ReverseMap();
            CreateMap<DeleteDepartmentCommand, GetByIdArgs>().ReverseMap();
            CreateMap<UsesrByDepartmentResponse, UsersByDepartmentDto>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
                .ReverseMap();
            CreateMap<UserInfoResponse, UserDto>().ReverseMap();

            CreateMap<DepartmentDto, ProjectsByDepartmentResponse>();
            CreateMap<ProjectDto, ProjectResponse>();
        }
    }
}

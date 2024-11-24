using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Response;
using ManagementSystem.WebApi.Models.Response.Role;
using ManagementSystem.WebApi.Models.Response.User;

namespace ManagementSystem.WebApi.MappingProfile.User
{
    public class UserMappingProfile : AutoMapper.Profile
    {
        public UserMappingProfile()
        {
            CreateMap<LoginDto, LoginResponse>();

            CreateMap<UserDto, UserResponse>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));
            
            CreateMap<ProjectDto, ProjectResponse>();
            CreateMap<ProjectDto, ProjectResponse>();
            CreateMap<DepartmentDto, DepartmentOfUserResponse>();
            CreateMap<RoleDto, RoleResponse>();
        }
    }
}

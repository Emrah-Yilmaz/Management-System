using CommonLibrary.Features.Paginations;
using CommonLibrary.Models.Args;
using ManagementSystem.Application.Features.Commands.Department;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Areas.Departments.Models.Responses;
using ManagementSystem.WebApi.Areas.Users.Models.Responses;

namespace ManagementSystem.WebApi.Areas.Departments.MappingProfile
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
        }
    }
}

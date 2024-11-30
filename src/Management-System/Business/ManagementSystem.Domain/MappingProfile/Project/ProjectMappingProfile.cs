using System.IO.Compression;
using AutoMapper;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.MappingProfile.Project;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<CreateProjectArgs, Domain.Entities.Project>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        
        CreateMap<Domain.Entities.Project, ProjectUsersDto>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users));

        CreateMap<Domain.Entities.User, UserDto>();
    }
}
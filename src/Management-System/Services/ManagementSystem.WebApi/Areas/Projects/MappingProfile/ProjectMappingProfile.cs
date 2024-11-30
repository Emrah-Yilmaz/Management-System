using AutoMapper;
using CommonLibrary.Features.Paginations;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Areas.Projects.Models.Responses;
using ManagementSystem.WebApi.Models.Response;

namespace ManagementSystem.WebApi.Areas.Projects.MappingProfile;
public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<PagedViewModel<ProjectDto>, PagedViewModel<ProjectResponse>>()
            .ForMember(dest => dest.Results, opt => opt.MapFrom(source => source.Results));
        CreateMap<ProjectDto, ProjectResponse>();
        CreateMap<ProjectUsersDto, ProjectUsersResponse>();
    }
}
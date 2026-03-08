using AutoMapper;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Response;
using ManagementSystem.WebApi.Areas.Admin.Projects.Models.Responses;

namespace ManagementSystem.WebApi.Areas.Admin.Projects.MappingProfile
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<ProjectDto, ProjectResponse>().ReverseMap();
            CreateMap<ProjectDto, GetProjectBasicResponse>().ReverseMap();
        }
    }
}
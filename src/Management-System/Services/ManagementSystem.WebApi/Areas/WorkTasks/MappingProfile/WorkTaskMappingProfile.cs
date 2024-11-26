using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Areas.WorkTasks.Models.Responses;

namespace ManagementSystem.WebApi.Areas.WorkTasks.MappingProfile
{
    public class WorkTaskMappingProfile : AutoMapper.Profile
    {
        public WorkTaskMappingProfile()
        {
            CreateMap<WorkTasksDto, WorkTasksResponse>();
        }
    }
}

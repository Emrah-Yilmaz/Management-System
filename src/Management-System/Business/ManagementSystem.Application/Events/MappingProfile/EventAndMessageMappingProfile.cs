using AutoMapper;
using CommonLibrary.Messages;
using ManagementSystem.Application.Events.DepartmentEvents;

namespace ManagementSystem.Application.Events.MappingProfile
{
    public class EventAndMessageMappingProfile : Profile
    {
        public EventAndMessageMappingProfile()
        {
            CreateMap<SendEmailEvent, CreatedDepartmentMessage>().ReverseMap();
        }
    }
}

using AutoMapper;
using CommonLibrary.Messages;
using ManagementSystem.Domain.Events.DepartmentEvents;

namespace ManagementSystem.Domain.Events.MappingProfile
{
    public class EventAndMessageMappingProfile : Profile
    {
        public EventAndMessageMappingProfile()
        {
            CreateMap<DepartmentCreated, SendEmailMessage>().ReverseMap();
        }
    }
}

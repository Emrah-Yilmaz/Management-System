using AutoMapper;
using CommonLibrary.Features.Paginations;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Areas.Admin.User.Models.Responses;

namespace ManagementSystem.WebApi.Areas.Admin.User.MappingProfile
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<PagedViewModel<UserResponse>, PagedViewModel<UserDto>>().ReverseMap();

        }
    }
}

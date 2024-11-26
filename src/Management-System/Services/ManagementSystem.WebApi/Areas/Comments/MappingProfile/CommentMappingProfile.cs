using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Areas.Comments.Models.Responses;

namespace ManagementSystem.WebApi.Areas.Comments.MappingProfile
{
    public class CommentMappingProfile : AutoMapper.Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<GetCommentDto, GetCommentResponse>();
        }
    }
}

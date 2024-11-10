using CommonLibrary.Models.Args;
using ManagementSystem.Domain.Models.Dto;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.Comment
{
    public class GetCommentQuery : GetByIdArgs, IRequest<GetCommentDto>
    {
    }
}

using CommonLibrary.Models.Args;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Comment
{
    public class DeleteCommentCommand : GetByIdArgs, IRequest<bool>
    {
    }
}

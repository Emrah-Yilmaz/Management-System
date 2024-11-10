using MediatR;

namespace ManagementSystem.Application.Features.Commands.Bogus
{
    public class CreateCommentToRandomTasksCommand : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}

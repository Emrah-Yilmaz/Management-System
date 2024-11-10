using ManagementSystem.Domain.Services.Abstract.User;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Bogus
{
    public class CreateCommentToRandomTasksCommandHandler : IRequestHandler<CreateCommentToRandomTasksCommand, bool>
    {
        private readonly IUserService _userService;

        public CreateCommentToRandomTasksCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(CreateCommentToRandomTasksCommand request, CancellationToken cancellationToken)
        {
            return await _userService.CreateCommentToRandomTasks(request.UserId, cancellationToken);
        }
    }
}

using ManagementSystem.Domain.Services.Abstract.User;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.User
{
    internal class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, bool>
    {
        private readonly IUserService _userService;

        public AssignRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            return await _userService.AssignRoleAsync(request, cancellationToken);
        }
    }
}

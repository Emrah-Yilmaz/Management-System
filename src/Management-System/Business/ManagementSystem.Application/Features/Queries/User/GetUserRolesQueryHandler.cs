using ManagementSystem.Domain.Services.Abstract.User;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.User
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, Domain.Entities.User>
    {
        private readonly IUserService _userService;
        public GetUserRolesQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Domain.Entities.User> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserRoles(request, cancellationToken);
        }
    }
}
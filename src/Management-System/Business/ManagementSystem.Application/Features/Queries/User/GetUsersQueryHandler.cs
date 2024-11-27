using CommonLibrary.Features.Paginations;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.User;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.User
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedViewModel<UserDto>>
    {
        private readonly IUserService _userService;

        public GetUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<PagedViewModel<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _userService.GetUsers(request, cancellationToken);
            return result;
        }
    }
}

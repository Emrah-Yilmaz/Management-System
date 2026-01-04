using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.User;
using MediatR;

namespace ManagementSystem.Application.Features.Queries.User
{
    public class GetUsersBasicQueryHandler : IRequestHandler<GetUsersBasicQuery, List<UserDto>>
    {
        private readonly IUserService _service;

        public GetUsersBasicQueryHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<List<UserDto>> Handle(GetUsersBasicQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetUsersBasicAsync(cancellationToken);
        }
    }
}

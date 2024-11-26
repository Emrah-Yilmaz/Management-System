using AutoMapper;
using ManagementSystem.Application.Features.Commands.User;
using ManagementSystem.Application.Features.Queries.User;
using ManagementSystem.Domain.TokenHandler;
using ManagementSystem.WebApi.Areas.Admin.User.Models.Responses;
using ManagementSystem.WebApi.Areas.Users.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Areas.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IDomainPrincipal _domainPrincipal;
        public UserController(IMediator mediator, IMapper mapper, IDomainPrincipal domainPrincipal)
        {
            _mediator = mediator;
            _mapper = mapper;
            _domainPrincipal = domainPrincipal;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command);

            if (result <= 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost("CreateAddress")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAddress([FromBody] CreateUserAddressCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }


        [HttpPut("UpdateAddress")]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAddress([FromBody] CreateUserAddressCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserProfile(CancellationToken cancellationToken = default)
        {
            var userId = _domainPrincipal.GetClaims()?.Id;
            if (!userId.HasValue)
                return BadRequest();

            var query = new GetUserQuery();
            query.UserId = userId.Value;
            var result = await _mediator.Send(query);
            var mappedResult = _mapper.Map<UserResponse>(result);
            return Ok(mappedResult);
        }
    }
}

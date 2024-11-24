using AutoMapper;
using ManagementSystem.Application.Features.Commands.User;
using ManagementSystem.Application.Features.Queries.User;
using ManagementSystem.Domain.TokenHandler;
using ManagementSystem.WebApi.Models.Request.User;
using ManagementSystem.WebApi.Models.Response.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Controllers
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
        public async Task<IActionResult> GetUser(CancellationToken cancellationToken = default)
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

        [HttpGet("users")]
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequest request, CancellationToken cancellationToken = default)
        {
            var query = new GetUsersQuery();
            query.UserRequestType = request.UserRequestType;

            var result = await _mediator.Send(query, cancellationToken);

            if (result is null || result.Count == 0)
            {
                return NotFound();
            }
            var mappedResponse = _mapper.Map<List<UserResponse>>(result);
            return Ok(mappedResponse);
        }

        [HttpGet("{userId}/roles")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoles([FromRoute] int userId, CancellationToken cancellationToken = default)
        {
            var query = new GetUserRolesQuery();
            query.Id = userId;
            var result = await _mediator.Send(query, cancellationToken);
            if (result is null)
            {
                return NotFound();
            }
            var mappedResponse = _mapper.Map<UserResponse>(result);
            return Ok(mappedResponse);
        }

    }
}

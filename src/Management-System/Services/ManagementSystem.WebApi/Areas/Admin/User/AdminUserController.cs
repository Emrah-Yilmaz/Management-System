using AutoMapper;
using ManagementSystem.Application.Features.Commands.User;
using ManagementSystem.Application.Features.Queries.User;
using ManagementSystem.WebApi.Areas.Admin.User.Models.Responses;
using ManagementSystem.WebApi.Areas.Users.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Areas.Admin.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AdminUserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPatch("change-status")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeStatus([FromQuery] ChangeStatusCommand request, CancellationToken cancellationToken = default)
        {
            var command = await _mediator.Send(request, cancellationToken);
            if (!command)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("assign-department")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> AssignDepartment(AddUserToDepartmentCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result)
                return BadRequest();

            return Ok(result);
        }
        [HttpPost("assign-project")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignProject(AssignProjectCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost("assign-role")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignRole(AssignRoleCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result)
                return BadRequest();

            return Ok(result);
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
        public async Task<IActionResult> GetUserRoles([FromRoute] int userId, CancellationToken cancellationToken = default)
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

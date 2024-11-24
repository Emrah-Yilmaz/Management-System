using ManagementSystem.Application.Features.Commands.User;
using ManagementSystem.Application.Features.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Controllers.Admin.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminSiteUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminSiteUserController(IMediator mediator)
        {
            _mediator = mediator;
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
        [HttpPost("assing-project")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignProject(AssignProjectCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result)
                return BadRequest();

            return Ok(result);
        }
    }
}

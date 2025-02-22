using ManagementSystem.Application.Features.Commands.CommonChangeStatus;
using ManagementSystem.Application.Features.Queries.User;
using ManagementSystem.WebApi.Areas.Base.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Areas.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{id}/change-status")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromQuery] ChangeStatusRequest request, CancellationToken cancellationToken = default)
        {
            var command = new ChangeStatusByModuleCommand()
            {
                Id = id,
                ModulesType = request.ModulesType,
                StatusType = request.StatusType
            };
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}

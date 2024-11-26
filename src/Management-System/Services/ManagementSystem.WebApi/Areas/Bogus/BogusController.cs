using ManagementSystem.Application.Features.Commands.Bogus;
using ManagementSystem.Domain.TokenHandler;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Areas.Bogus
{
    [Route("api/[controller]")]
    [ApiController]
    public class BogusController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDomainPrincipal _domainPrincipal;

        public BogusController(IMediator mediator, IDomainPrincipal domainPrincipal)
        {
            _mediator = mediator;
            _domainPrincipal = domainPrincipal;
        }

        [HttpPost("CreateBogusData")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateBogusData(CancellationToken cancellationToken = default)
        {
            var command = new CreateEntityWithBogusCommand();
            var result = await _mediator.Send(command);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost("CreateCommentToRandomTasks")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCommentToRandomTasks(CancellationToken cancellationToken = default)
        {
            var command = new CreateCommentToRandomTasksCommand();
            var userId = _domainPrincipal.GetClaims().Id;
            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}

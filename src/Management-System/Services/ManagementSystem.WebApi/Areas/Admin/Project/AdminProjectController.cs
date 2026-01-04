using AutoMapper;
using ManagementSystem.Application.Features.Queries.Department;
using ManagementSystem.Application.Features.Queries.Project;
using ManagementSystem.WebApi.Areas.Admin.Projects.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Areas.Admin.Project
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AdminProjectController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet("basic")]
        [ProducesResponseType(typeof(List<GetProjectBasicResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepartmentsBasicAsync(CancellationToken cancellationToken = default)
        {
            var query = new GetProjectsBasicQuery();
            var result = await _mediator.Send(query, cancellationToken);
            if (result is null || result.Count == 0)
            {
                return NotFound();
            }
            var mappedResponse = _mapper.Map<List<GetProjectBasicResponse>>(result);
            return Ok(mappedResponse);
        }
    }
}

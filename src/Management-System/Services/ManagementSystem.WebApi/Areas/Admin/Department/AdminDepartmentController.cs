using AutoMapper;
using ManagementSystem.Application.Features.Queries.Department;
using ManagementSystem.WebApi.Areas.Departments.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Areas.Admin.Departments
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AdminDepartmentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("basic")]
        [ProducesResponseType(typeof(List<DepartmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepartmentsBasicAsync(CancellationToken cancellationToken = default)
        {
            var query = new GetDepartmentsBasicQuery();
            var result = await _mediator.Send(query, cancellationToken);
            if (result is null || result.Count == 0)
            {
                return NotFound();
            }
            var mappedResponse = _mapper.Map<List<DepartmentResponse>>(result);
            return Ok(mappedResponse);
        }
    }
}
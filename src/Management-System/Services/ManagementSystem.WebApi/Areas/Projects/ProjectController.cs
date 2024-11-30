using AutoMapper;
using AutoMapper.Configuration.Annotations;
using CommonLibrary.Features.Paginations;
using ManagementSystem.Application.Features.Queries.Project;
using ManagementSystem.WebApi.Areas.Base;
using ManagementSystem.WebApi.Areas.Projects.Models.Requests;
using ManagementSystem.WebApi.Areas.Projects.Models.Responses;
using ManagementSystem.WebApi.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Areas.Projects;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProjectController(IMediator mediator, IMapper mapper) : base(mediator)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    [HttpPost()]
    public async Task<IActionResult> Create(CreateProjectRequest request, CancellationToken cancellationToken = default)
    {
        var command = new CreateProjectCommand()
        {
            Name = request.Name,
            DepartmentId = request.DepartmentId
        };
        await _mediator.Send(command, cancellationToken);
        return Created();
    }

    [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<ProjectResponse>), StatusCodes.Status201Created)]
    [HttpGet()]
    public async Task<IActionResult> Projects([FromQuery] SearchProjectsRequest request, CancellationToken cancellationToken = default)
    {
        var query = new GetProjectsQuery()
        {
            Name = request.Name ?? null,
            Page = request.Page,
            PageSize = request.PageSize
        };

        var result = await _mediator.Send(query, cancellationToken);
        if (result is null || result.Results.Count == 0)
        {
            return NotFound();
        }

        var mappedResponse = _mapper.Map<PagedViewModel<ProjectResponse>>(result);
        return Ok(mappedResponse);
    }

    [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProjectUsersResponse), StatusCodes.Status201Created)]
    [HttpGet("{id}/projects")]
    public async Task<IActionResult> GetProject([FromRoute] int id, [FromQuery] GetProjectRequest request, CancellationToken cancellationToken = default)
    {
        var query = new GetProjectQuery()
        {
            Id = id,
            Type = request.Type
        };
        var result = await _mediator.Send(query, cancellationToken);
        if (result is null)
            return NotFound();

        var mappedResponse = _mapper.Map<ProjectUsersResponse>(result);
        return Ok(mappedResponse);
    }
}
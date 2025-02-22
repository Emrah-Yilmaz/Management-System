﻿using AutoMapper;
using CommonLibrary.Features.Paginations;
using ManagementSystem.Application.Features.Commands.Department;
using ManagementSystem.Application.Features.Queries.Department;
using ManagementSystem.WebApi.Areas.Base;
using ManagementSystem.WebApi.Areas.Departments.Models.Requests;
using ManagementSystem.WebApi.Areas.Departments.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Areas.Departments
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public DepartmentController(IMapper mapper, IMediator mediator) : base(mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost()]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Add([FromBody] CreateDepartmentCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut()]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateDepartmentCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken = default)
        {
            var command = new DeleteDepartmentCommand
            {
                Id = Id
            };
            var result = await _mediator.Send(command);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(DepartmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepartment([FromRoute] GetDepartmentQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request);
            if (result is null)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<DepartmentResponse>(result);

            return Ok(mappedResponse);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(PagedViewModel<DepartmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepartments([FromQuery] DepartmentRequest request, CancellationToken cancellationToken = default)
        {
            var query = new GetDeparmentsQuery
            {
                Page = request.Page,
                PageSize = request.PageSize,
                Name = request.Name
            };

            var result = await _mediator.Send(query);
            if (result is null || result.Results.Count == 0)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<PagedViewModel<DepartmentResponse>>(result);

            return Ok(mappedResponse);
        }

        [HttpGet("{departmentId}/users")]
        [ProducesResponseType(typeof(UsesrByDepartmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersByDepartment([FromQuery] UsersByDepartmentQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request);
            if (result is null)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<UsesrByDepartmentResponse>(result);

            return Ok(mappedResponse);
        }
        [HttpGet("{departmentId}/projects")]
        [ProducesResponseType(typeof(ProjectsByDepartmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectsByDepartment([FromRoute] int departmentId, CancellationToken cancellationToken = default)
        {
            var query = new GetProjectsByDepartmentQuery();
            query.Id = departmentId;
            var result = await _mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<ProjectsByDepartmentResponse>(result);

            return Ok(mappedResponse);
        }
    }
}

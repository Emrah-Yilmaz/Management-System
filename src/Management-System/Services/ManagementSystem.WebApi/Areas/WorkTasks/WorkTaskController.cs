﻿using AutoMapper;
using ManagementSystem.Application.Features.Commands.Department;
using ManagementSystem.Application.Features.Commands.WorkTask;
using ManagementSystem.Application.Features.Queries.Department;
using ManagementSystem.Application.Features.Queries.WorkTask;
using ManagementSystem.WebApi.Areas.Base;
using ManagementSystem.WebApi.Areas.WorkTasks.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.WebApi.Areas.WorkTasks
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class WorkTaskController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public WorkTaskController(IMediator mediator, IMapper mapper) : base(mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<WorkTasksResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] GetWorkTasksQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request);

            if (result is null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTask([FromRoute] GetWorkTaskQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request);
            if (result is null)
            {
                return NotFound();
            }


            return Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> Craate([FromBody] CreateWorkTaskCommand request, CancellationToken cancellationToken = default)
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
        public async Task<IActionResult> Update([FromBody] UpdateWorkTaskCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }

    }
}

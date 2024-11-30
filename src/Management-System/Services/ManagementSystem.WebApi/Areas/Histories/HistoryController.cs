using AutoMapper;
using ManagementSystem.Application.Features.Queries.History;
using ManagementSystem.WebApi.Areas.Base;
using ManagementSystem.WebApi.Areas.Histories.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Areas.Histories
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : BaseController
    {
        private readonly IMediator _meditor;
        private readonly IMapper _mapper;

        public HistoryController(IMediator mediator, IMapper mapper) : base(mediator)
        {
            _meditor = mediator;
            _mapper = mapper;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(HistoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser([FromQuery] HistoryQuery request, CancellationToken cancellationToken = default)
        {
            var query = await _meditor.Send(request, cancellationToken);
            if (query is null || query.Count == 0)
            {
                return NotFound();
            }

            var mappedResponse = _mapper.Map<HistoryResponse>(query);
            return Ok(mappedResponse);
        }
    }
}

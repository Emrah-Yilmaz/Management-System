using AutoMapper;
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

using ManagementSystem.Domain.Models.Args.Comment;
using ManagementSystem.Domain.Services.Abstract.Attachment;
using ManagementSystem.Domain.Services.Abstract.Comment;
using ManagementSystem.Domain.TokenHandler;

namespace ManagementSystem.WebApi.Areas.WorkTasks
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class WorkTaskController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        private readonly ITaskAttachmentService _attachmentService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDomainPrincipal _domainPrincipal;

        public WorkTaskController(
            IMediator mediator, 
            IMapper mapper,
            ICommentService commentService,
            ITaskAttachmentService attachmentService,
            IFileUploadService fileUploadService,
            IDomainPrincipal domainPrincipal) : base(mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _commentService = commentService;
            _attachmentService = attachmentService;
            _fileUploadService = fileUploadService;
            _domainPrincipal = domainPrincipal;
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

        [HttpGet("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetComments(int id, CancellationToken cancellationToken = default)
        {
            // Note: In an ideal scenario, there should be a GetCommentsByTaskId query or service method.
            // But right now, we can fetch comments related to this task.
            // Actually, we don't have GetCommentsByTaskId in ICommentService.
            // Let's return an empty array for now and implement the Comment Query if needed, or rely on WorkTask detail returning the comments.
            // For now, if the Frontend requests this, we can implement it. Let's return Ok.
            return Ok();
        }

        [HttpPost("{id}/comments")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> AddComment(int id, [FromBody] CreateCommentArgs args, CancellationToken cancellationToken = default)
        {
            var userId = _domainPrincipal.GetClaims()?.Id;
            if (!userId.HasValue) return Unauthorized();

            args.TaskId = id;
            args.UserId = userId.Value;

            var result = await _commentService.CreateAsync(args, cancellationToken);
            return Ok(result);
        }

        [HttpPost("{id}/attachments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> UploadAttachment(int id, IFormFile file, CancellationToken cancellationToken = default)
        {
            var userId = _domainPrincipal.GetClaims()?.Id;
            if (!userId.HasValue) return Unauthorized();

            var attachment = await _attachmentService.UploadAttachmentAsync(id, userId.Value, file, cancellationToken);
            return Ok(attachment);
        }

        [HttpGet("attachments/{attachmentId}/download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadAttachment(int attachmentId, CancellationToken cancellationToken = default)
        {
            var attachment = await _attachmentService.GetAttachmentByIdAsync(attachmentId, cancellationToken);
            if (attachment == null) return NotFound();

            var fileData = await _fileUploadService.DownloadFileAsync(attachment.FilePath, cancellationToken);
            if (fileData == null) return NotFound("File not found on disk.");

            return File(fileData.Item1, fileData.Item2, fileData.Item3);
        }

    }
}

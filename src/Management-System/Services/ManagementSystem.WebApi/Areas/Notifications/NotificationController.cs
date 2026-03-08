using ManagementSystem.Domain.Models.Args.Notification;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Services.Abstract.Notification;
using ManagementSystem.Domain.TokenHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IDomainPrincipal _domainPrincipal;

        public NotificationController(INotificationService notificationService, IDomainPrincipal domainPrincipal)
        {
            _notificationService = notificationService;
            _domainPrincipal = domainPrincipal;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<NotificationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyNotifications(CancellationToken cancellationToken = default)
        {
            var userId = _domainPrincipal.GetClaims()?.Id;
            if (!userId.HasValue) return Unauthorized();

            var result = await _notificationService.GetUserNotificationsAsync(userId.Value, cancellationToken);
            return Ok(result);
        }

        [HttpGet("unread-count")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnreadCount(CancellationToken cancellationToken = default)
        {
            var userId = _domainPrincipal.GetClaims()?.Id;
            if (!userId.HasValue) return Unauthorized();

            var count = await _notificationService.GetUnreadCountAsync(userId.Value, cancellationToken);
            return Ok(count);
        }

        [HttpPatch("{id}/read")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> MarkAsRead(int id, CancellationToken cancellationToken = default)
        {
            var result = await _notificationService.MarkAsReadAsync(id, cancellationToken);
            if (!result) return NotFound();

            return Ok(result);
        }

        [HttpPost("read-all")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> MarkAllAsRead(CancellationToken cancellationToken = default)
        {
            var userId = _domainPrincipal.GetClaims()?.Id;
            if (!userId.HasValue) return Unauthorized();

            var result = await _notificationService.MarkAllAsReadAsync(userId.Value, cancellationToken);
            return Ok(result);
        }
    }
}

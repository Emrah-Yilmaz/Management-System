using ManagementSystem.Domain.Ports;
using Microsoft.AspNetCore.SignalR;

namespace ManagementSystem.WebApi.Hubs
{
    public class HubPublisher : IHubPublisher
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public HubPublisher(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(int userId, string title, string message)
        {
            await _hubContext.Clients.Group($"User_{userId}").SendAsync("ReceiveNotification", new { Title = title, Message = message });
        }
    }
}

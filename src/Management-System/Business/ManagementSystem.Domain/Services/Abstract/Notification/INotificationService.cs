using ManagementSystem.Domain.Models.Args.Notification;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Services.Abstract.Notification
{
    public interface INotificationService : IDomainService
    {
        Task<List<NotificationDto>> GetUserNotificationsAsync(int userId, CancellationToken cancellationToken = default);
        Task<int> GetUnreadCountAsync(int userId, CancellationToken cancellationToken = default);
        Task<bool> MarkAsReadAsync(int notificationId, CancellationToken cancellationToken = default);
        Task<bool> MarkAllAsReadAsync(int userId, CancellationToken cancellationToken = default);
        Task<int> CreateAsync(CreateNotificationArgs args, CancellationToken cancellationToken = default);
    }
}

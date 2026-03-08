using AutoMapper;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Args.Notification;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Persistence.Notification;
using ManagementSystem.Domain.Services.Abstract.Notification;

namespace ManagementSystem.Domain.Services.Concrete.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateNotificationArgs args, CancellationToken cancellationToken = default)
        {
            var entity = new Domain.Entities.Notification
            {
                UserId = args.UserId,
                Title = args.Title,
                Message = args.Message,
                Type = args.Type,
                IsRead = false
            };
            
            return await _notificationRepository.AddAsync(entity, cancellationToken);
        }

        public async Task<int> GetUnreadCountAsync(int userId, CancellationToken cancellationToken = default)
        {
            var notifications = await _notificationRepository.GetListAsync(
                predicate: n => n.UserId == userId && !n.IsRead,
                noTracking: true,
                includes: null);

            return notifications?.Count ?? 0;
        }

        public async Task<List<NotificationDto>> GetUserNotificationsAsync(int userId, CancellationToken cancellationToken = default)
        {
            var notifications = await _notificationRepository.GetListAsync(
                predicate: n => n.UserId == userId,
                noTracking: true,
                orderBy: q => q.OrderByDescending(n => n.CreatedOn),
                includes: null);

            if (notifications == null || notifications.Count == 0)
                return new List<NotificationDto>();

            var dtos = notifications.Select(n => new NotificationDto
            {
                Id = n.Id,
                UserId = n.UserId,
                Title = n.Title,
                Message = n.Message,
                IsRead = n.IsRead,
                Type = n.Type,
                CreatedOn = n.CreatedOn
            }).ToList();

            return dtos;
        }

        public async Task<bool> MarkAllAsReadAsync(int userId, CancellationToken cancellationToken = default)
        {
            var notifications = await _notificationRepository.GetListAsync(
                predicate: n => n.UserId == userId && !n.IsRead,
                noTracking: false,
                includes: null);

            if (notifications == null || notifications.Count == 0)
                return true;

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            var result = await _notificationRepository.SaveChangeAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> MarkAsReadAsync(int notificationId, CancellationToken cancellationToken = default)
        {
            var notification = await _notificationRepository.GetByIdAsync(
                id: notificationId,
                noTracking: false,
                cancellationToken: cancellationToken);

            if (notification == null)
                return false;

            notification.IsRead = true;
            var result = await _notificationRepository.SaveChangeAsync(cancellationToken);
            return result > 0;
        }
    }
}

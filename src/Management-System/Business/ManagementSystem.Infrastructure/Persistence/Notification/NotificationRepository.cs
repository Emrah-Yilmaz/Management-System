using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Persistence.Notification;
using ManagementSystem.Infrastructure.Context;

namespace ManagementSystem.Infrastructure.Persistence.Notification
{
    public class NotificationRepository : Repository<Domain.Entities.Notification>, INotificationRepository
    {
        private readonly AppDbContext _dbContext;

        public NotificationRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

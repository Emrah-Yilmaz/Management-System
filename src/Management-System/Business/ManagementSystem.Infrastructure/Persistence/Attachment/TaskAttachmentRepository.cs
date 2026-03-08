using ManagementSystem.Domain.Persistence.Attachment;
using ManagementSystem.Infrastructure.Context;

namespace ManagementSystem.Infrastructure.Persistence.Attachment
{
    public class TaskAttachmentRepository : Repository<Domain.Entities.TaskAttachment>, ITaskAttachmentRepository
    {
        public TaskAttachmentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}

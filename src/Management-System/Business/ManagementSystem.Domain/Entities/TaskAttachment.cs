using Packages.Repositories.EfCore.Entity;

namespace ManagementSystem.Domain.Entities
{
    public class TaskAttachment : BaseEntity
    {
        public int WorkTaskId { get; set; }
        public virtual WorkTask? WorkTask { get; set; }
        
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
    }
}

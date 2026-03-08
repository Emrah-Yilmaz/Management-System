using Packages.Repositories.EfCore.Entity;

namespace ManagementSystem.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        
        public string Type { get; set; } // e.g: "TaskAssigned", "Deadline", "System"
    }
}

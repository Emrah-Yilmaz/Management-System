namespace ManagementSystem.Domain.Models.Dto
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string Type { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}

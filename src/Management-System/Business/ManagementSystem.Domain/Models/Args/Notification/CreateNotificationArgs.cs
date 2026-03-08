namespace ManagementSystem.Domain.Models.Args.Notification
{
    public class CreateNotificationArgs
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }
}

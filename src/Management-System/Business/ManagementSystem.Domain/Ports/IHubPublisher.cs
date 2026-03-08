namespace ManagementSystem.Domain.Ports
{
    public interface IHubPublisher
    {
        Task SendNotificationAsync(int userId, string title, string message);
    }
}

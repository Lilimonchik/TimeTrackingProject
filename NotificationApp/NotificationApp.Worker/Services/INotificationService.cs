namespace NotificationApp.Worker.Services
{
    public interface INotificationService
    {
        Task HandleNotificationAsync(string message);
    }
}

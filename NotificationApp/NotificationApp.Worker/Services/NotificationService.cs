using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace NotificationApp.Worker.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public async Task HandleNotificationAsync(string message)
        {
            _logger.LogInformation($"Handling notification: {message}");

            await Task.Delay(500); 

            _logger.LogInformation($"Notification processed: {message}");
        }
    }
}

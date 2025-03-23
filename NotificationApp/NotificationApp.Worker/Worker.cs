using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using NotificationApp.Worker.Services;

namespace NotificationApp.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, INotificationService notificationService, IConfiguration configuration)
        {
            _logger = logger;
            _notificationService = notificationService;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var factory = new ConnectionFactory() { HostName = _configuration.GetValue<string>("Hosts:RabbitMQHost")! };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: "activityQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                await _notificationService.HandleNotificationAsync(message);
            };

            await channel.BasicConsumeAsync(queue: "activityQueue", autoAck: true, consumer: consumer);
            _logger.LogInformation("Listening for activity messages...");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
            }
        }
    }
}

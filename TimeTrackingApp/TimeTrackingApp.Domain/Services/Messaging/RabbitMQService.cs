using RabbitMQ.Client;
using System.Text;


namespace TimeTrackingApp.Domain.Services.Messaging
{
    public class RabbitMQService
    {
        private readonly string _hostname = "localhost";

        public async void SendMessage(string message, CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync("activityQueue", false, false, false);

            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "activityQueue",
                body: body,
                cancellationToken
            );

            Console.WriteLine($"[x] Sent {message}");
        }
    }
}

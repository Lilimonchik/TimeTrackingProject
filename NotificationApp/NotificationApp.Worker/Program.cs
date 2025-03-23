using NotificationApp.Worker;
using NotificationApp.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<INotificationService, NotificationService>();

var host = builder.Build();
host.Run();

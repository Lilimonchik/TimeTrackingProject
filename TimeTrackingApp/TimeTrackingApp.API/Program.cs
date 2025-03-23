using TimeTrackingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TimeTrackingApp.Domain.Repositories;
using TimeTrackingApp.Infrastructure.Repositories;
using TimeTrackingApp.Domain.Services.Projects;
using TimeTrackingApp.Domain.Services.Activities;
using TimeTrackingApp.Domain.Services.ActivityTypes;
using TimeTrackingApp.Domain.Services.Employees;
using TimeTrackingApp.Domain.Services.Roles;
using FluentValidation;
using FluentValidation.AspNetCore;
using TimeTrackingApp.Domain.Validations;
using TimeTrackingApp;
using TimeTrackingApp.Domain.Services.Messaging;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TimeTracking",
        Version = "v1"
    });
    c.CustomSchemaIds(type => type.FullName);
});
builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IActivityTypeRepository, ActivityTypeRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IActivityTypeService, ActivityTypeService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RoleDtoValidator>());

builder.Services.AddSingleton<RabbitMQService>();

builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Time Tracking API V1");
});
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var environment = services.GetRequiredService<IHostEnvironment>();
    DbSeeder.Initialize(services, environment);
}


app.Run();

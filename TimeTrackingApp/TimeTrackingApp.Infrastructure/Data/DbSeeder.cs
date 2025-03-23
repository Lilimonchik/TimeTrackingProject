using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Infrastructure.Data;

namespace TimeTrackingApp
{
    public static class DbSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider, IHostEnvironment environment)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate(); 

            if (!context.Activities.Any())
            {
                context.Activities.AddRange(
                    new Activity
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                        Hours = 5,
                    },
                    new Activity
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now.AddDays(1),
                        Hours = 7,
                    }
                );
            }

            if (!context.Employees.Any())
            {
                context.Employees.AddRange(
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        Name = "John Doe",
                        Sex ="Male"
                    },
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        Name = "Jane Smith",
                        Sex = "Female"
                    }
                );
            }

            if (!context.Projects.Any())
            {
                context.Projects.AddRange(
                    new Project
                    {
                        Id = Guid.NewGuid(),
                        Name = "Project A",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddMonths(2)
                    },
                    new Project
                    {
                        Id = Guid.NewGuid(),
                        Name = "Project B",
                        StartDate = DateTime.Now.AddMonths(-1),
                        EndDate = DateTime.Now.AddMonths(1)
                    }
                );
            }

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = "Developer",
                    },
                    new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = "Manager",
                    }
                );
            }

            if (!context.ActivityTypes.Any())
            {
                context.ActivityTypes.AddRange(
                    new ActivityType
                    {
                        Id = Guid.NewGuid(),
                        Name = "Development",
                    },
                    new ActivityType
                    {
                        Id = Guid.NewGuid(),
                        Name = "Testing",
                    }
                );
            }

            context.SaveChanges();
        }
    }
}

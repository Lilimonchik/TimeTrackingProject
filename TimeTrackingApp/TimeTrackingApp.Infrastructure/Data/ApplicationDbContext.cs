using Microsoft.EntityFrameworkCore;
using TimeTrackingApp.Domain.Model;

namespace TimeTrackingApp.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Project>()
                .HasMany(p => p.Activities)
                .WithOne(a => a.Project)
                .HasForeignKey(a => a.ProjectId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Activities)
                .WithOne(a => a.Employee)
                .HasForeignKey(a => a.EmployeeId);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Activities)
                .WithOne(a => a.Role)
                .HasForeignKey(a => a.RoleId);

            modelBuilder.Entity<ActivityType>()
                .HasMany(at => at.Activities)
                .WithOne(a => a.ActivityType)
                .HasForeignKey(a => a.ActivityTypeId);

            modelBuilder.Entity<Employee>()
            .HasMany(e => e.Roles)
            .WithMany(r => r.Employees)
            .UsingEntity(j => j.ToTable("EmployeeRoles"));

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Projects)
                .WithMany(p => p.Employees)
                .UsingEntity(j => j.ToTable("EmployeeProjects"));

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ActivityTypes)
                .WithMany(at => at.Employees)
                .UsingEntity(j => j.ToTable("EmployeeActivityTypes"));

            modelBuilder.Entity<Project>()
                .HasMany(p => p.ActivityTypes)
                .WithMany(at => at.Projects)
                .UsingEntity(j => j.ToTable("ProjectActivityTypes"));
        }
    }
}

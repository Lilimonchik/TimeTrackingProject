namespace TimeTrackingApp.Domain.Model
{
    public class Activity
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public Guid? ProjectId { get; set; }
        public Project? Project { get; set; }

        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }

        public Guid? ActivityTypeId { get; set; } 
        public ActivityType? ActivityType { get; set; } 

        public DateTime Date { get; set; } 
        public double Hours { get; set; } 
    }
}

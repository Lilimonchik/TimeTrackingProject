using System.Data;

namespace TimeTrackingApp.Dto
{
    public class ActivityDto
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public EmployeeDto? Employee { get; set; }

        public Guid? ProjectId { get; set; }
        public ProjectDto? Project { get; set; }

        public Guid? RoleId { get; set; }
        public RoleDto? Role { get; set; }

        public Guid? ActivityTypeId { get; set; }
        public ActivityTypeDto? ActivityType { get; set; }

        public DateTime Date { get; set; }
        public double Hours { get; set; }
    }
}

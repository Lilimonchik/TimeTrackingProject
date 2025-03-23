using System.Data;
using System.Diagnostics;

namespace TimeTrackingApp.Dto
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<ActivityDto>? Activities { get; set; }
        public ICollection<RoleDto>? Roles { get; set; }
        public ICollection<ProjectDto>? Projects { get; set; }
        public ICollection<ActivityTypeDto>? ActivityTypes { get; set; }
    }
}

namespace TimeTrackingApp.Dto
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<ActivityDto>? Activities { get; set; }
        public ICollection<EmployeeDto>? Employees { get; set; }
        public ICollection<ActivityTypeDto>? ActivityTypes { get; set; }
    }
}

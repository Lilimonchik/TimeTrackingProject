namespace TimeTrackingApp.Dto
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ActivityDto>? Activities { get; set; }
        public ICollection<EmployeeDto>? Employees { get; set; }
    }
}

namespace TimeTrackingApp.Domain.Model
{
    public class ActivityType
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public ICollection<Activity>? Activities { get; set; }
        public ICollection<Employee>? Employees { get; set; }
        public ICollection<Project>? Projects { get; set; }
    }
}

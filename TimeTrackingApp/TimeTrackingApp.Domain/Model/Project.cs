namespace TimeTrackingApp.Domain.Model
{
    public class Project
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public ICollection<Activity>? Activities { get; set; }
        public ICollection<Employee>? Employees { get; set; }
        public ICollection<ActivityType>? ActivityTypes { get; set; }
    }
}
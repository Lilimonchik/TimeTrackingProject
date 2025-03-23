namespace TimeTrackingApp.Domain.Model
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; } 
        public DateTime BirthDate { get; set; }
        public ICollection<Role>? Roles { get; set; }
        public ICollection<Project>? Projects { get; set; }
        public ICollection<Activity>? Activities { get; set; }
        public ICollection<ActivityType>? ActivityTypes { get; set; }
    }
}

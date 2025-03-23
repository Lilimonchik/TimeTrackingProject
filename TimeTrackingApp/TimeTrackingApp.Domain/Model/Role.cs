namespace TimeTrackingApp.Domain.Model
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Activity>? Activities { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}

using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellation);
        Task<EmployeeDto> GetEmployeeByIdAsync(Guid id, CancellationToken cancellation);
        Task AddEmployeeAsync(EmployeeDto employee, CancellationToken cancellation);
        Task UpdateEmployeeAsync(EmployeeDto employee, CancellationToken cancellation);
        Task DeleteEmployeeAsync(Guid id, CancellationToken cancellation);
        Task<IEnumerable<EmployeeDto>> GetEmployeesByRoleAsync(string roleName, CancellationToken cancellation);
    }
}

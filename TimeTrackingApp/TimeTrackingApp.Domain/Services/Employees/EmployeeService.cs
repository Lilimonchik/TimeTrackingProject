using AutoMapper;
using Microsoft.Extensions.Logging;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Domain.Repositories;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task AddEmployeeAsync(EmployeeDto employee, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Starting to add a new employee.");
                if (employee == null)
                {
                    throw new ArgumentException("Employee cannot be null");
                }

                await _employeeRepository.AddAsync(_mapper.Map<Employee>(employee), cancellation);
                _logger.LogInformation($"Successfully added employee with ID: {employee.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new employee.");
                throw new ApplicationException("Failed to add employee.", ex);
            }
        }

        public async Task DeleteEmployeeAsync(Guid id, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation($"Starting to delete employee with ID: {id}");
                await _employeeRepository.DeleteAsync(id, cancellation);
                _logger.LogInformation($"Successfully deleted employee with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting employee with ID: {id}");
                throw new ApplicationException($"Failed to delete employee with ID: {id}", ex);
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("Starting to get all employees.");
                var employees = _mapper.Map<IEnumerable<EmployeeDto>>(await _employeeRepository.FindAsync(null, cancellation));
                _logger.LogInformation($"Successfully retrieved {employees.Count()} employees.");
                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all employees.");
                throw new ApplicationException("Failed to get all employees.", ex);
            }
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(Guid id, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation($"Starting to get employee by ID: {id}");
                var employee = _mapper.Map<EmployeeDto>(await _employeeRepository.FindAsync(x => x.Id == id, cancellation));
                if (employee == null)
                {
                    _logger.LogWarning($"Employee with ID {id} not found.");
                }
                _logger.LogInformation($"Successfully retrieved employee with ID: {id}");
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting employee by ID: {id}");
                throw new ApplicationException($"Failed to get employee with ID: {id}", ex);
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByRoleAsync(string roleName, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation($"Starting to get employees with role: {roleName}");
                var employees = _mapper.Map<IEnumerable<EmployeeDto>>(await _employeeRepository.FindAsync(x => x.Roles.Any(z => z.Name == roleName), cancellation));
                _logger.LogInformation($"Successfully retrieved employees with role: {roleName}");
                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting employees with role: {roleName}");
                throw new ApplicationException($"Failed to get employees with role: {roleName}", ex);
            }
        }

        public async Task UpdateEmployeeAsync(EmployeeDto employee, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation($"Starting to update employee with ID: {employee.Id}");
                if (employee == null)
                {
                    throw new ArgumentException("Employee cannot be null");
                }

                await _employeeRepository.UpsertAsync(_mapper.Map<Employee>(employee), cancellation);
                _logger.LogInformation($"Successfully updated employee with ID: {employee.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating employee with ID: {employee.Id}");
                throw new ApplicationException($"Failed to update employee with ID: {employee.Id}", ex);
            }
        }
    }
}

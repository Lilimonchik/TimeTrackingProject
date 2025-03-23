using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Domain.Services.Employees;
using TimeTrackingApp.Dto;
using FluentValidation;

namespace TimeTrackingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IValidator<EmployeeDto> _employeeValidator;

        public EmployeeController(IEmployeeService employeeService, IValidator<EmployeeDto> employeeValidator)
        {
            _employeeService = employeeService;
            _employeeValidator = employeeValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetAllEmployeesAsync(cancellationToken);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employee, CancellationToken cancellationToken)
        {
            if (employee == null)
            {
                return BadRequest("Employee cannot be null.");
            }

            var validationResult = await _employeeValidator.ValidateAsync(employee, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _employeeService.AddEmployeeAsync(employee, cancellationToken);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeDto employee, CancellationToken cancellationToken)
        {
            if (employee == null)
            {
                return BadRequest("Employee cannot be null.");
            }

            var existingEmployee = await _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            var validationResult = await _employeeValidator.ValidateAsync(employee, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _employeeService.UpdateEmployeeAsync(employee, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteEmployeeAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("role/{role}")]
        public async Task<IActionResult> GetEmployeesByRole(string role, CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetEmployeesByRoleAsync(role, cancellationToken);
            return Ok(employees);
        }
    }
}

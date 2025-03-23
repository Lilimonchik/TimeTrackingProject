using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Domain.Services.Roles;
using TimeTrackingApp.Dto;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace TimeTrackingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IValidator<RoleDto> _roleValidator;

        public RoleController(IRoleService roleService, IValidator<RoleDto> roleValidator)
        {
            _roleService = roleService;
            _roleValidator = roleValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetAllRolesAsync(cancellationToken);
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id, CancellationToken cancellationToken)
        {
            var role = await _roleService.GetRoleByIdAsync(id, cancellationToken);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] RoleDto role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                return BadRequest("Role cannot be null.");
            }

            var validationResult = await _roleValidator.ValidateAsync(role, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _roleService.AddRoleAsync(role, cancellationToken);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] RoleDto role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                return BadRequest("Role cannot be null.");
            }

            var existingRole = await _roleService.GetRoleByIdAsync(id, cancellationToken);
            if (existingRole == null)
            {
                return NotFound();
            }

            var validationResult = await _roleValidator.ValidateAsync(role, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _roleService.UpdateRoleAsync(role, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id, CancellationToken cancellationToken)
        {
            var existingRole = await _roleService.GetRoleByIdAsync(id, cancellationToken);
            if (existingRole == null)
            {
                return NotFound();
            }

            await _roleService.DeleteRoleAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetRolesByName(string name, CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetRolesByNameAsync(name, cancellationToken);
            return Ok(roles);
        }
    }
}

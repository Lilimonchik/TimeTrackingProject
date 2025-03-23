using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Domain.Services.Projects;
using TimeTrackingApp.Dto;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace TimeTrackingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IValidator<ProjectDto> _projectValidator;

        public ProjectController(IProjectService projectService, IValidator<ProjectDto> projectValidator)
        {
            _projectService = projectService;
            _projectValidator = projectValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects(CancellationToken cancellationToken)
        {
            var projects = await _projectService.GetAllProjectsAsync(cancellationToken);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id, CancellationToken cancellationToken)
        {
            var project = await _projectService.GetProjectByIdAsync(id, cancellationToken);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] ProjectDto project, CancellationToken cancellationToken)
        {
            if (project == null)
            {
                return BadRequest("Project cannot be null.");
            }

            var validationResult = await _projectValidator.ValidateAsync(project, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _projectService.AddProjectAsync(project, cancellationToken);
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectDto project, CancellationToken cancellationToken)
        {
            if (project == null)
            {
                return BadRequest("Project cannot be null.");
            }

            var existingProject = await _projectService.GetProjectByIdAsync(id, cancellationToken);
            if (existingProject == null)
            {
                return NotFound();
            }

            var validationResult = await _projectValidator.ValidateAsync(project, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _projectService.UpdateProjectAsync(project, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id, CancellationToken cancellationToken)
        {
            var existingProject = await _projectService.GetProjectByIdAsync(id, cancellationToken);
            if (existingProject == null)
            {
                return NotFound();
            }

            await _projectService.DeleteProjectAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetProjectsByEmployeeId(Guid employeeId, CancellationToken cancellationToken)
        {
            var projects = await _projectService.GetProjectsByEmployeeIdAsync(employeeId, cancellationToken);
            return Ok(projects);
        }
    }
}

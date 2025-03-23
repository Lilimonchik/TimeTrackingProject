using AutoMapper;
using Microsoft.Extensions.Logging;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Domain.Repositories;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Services.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<ProjectService> _logger;
        private readonly IMapper _mapper;
        public ProjectService(IProjectRepository projectRepository, ILogger<ProjectService> logger, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task AddProjectAsync(ProjectDto project, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting to add a new project.");
                if (project == null)
                {
                    throw new ArgumentException("Project cannot be null");
                }
                var data = _mapper.Map<Project>(project);

                await _projectRepository.AddAsync(_mapper.Map<Project>(project), cancellationToken);
                _logger.LogInformation($"Successfully added project with ID: {project.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new project.");
                throw new ApplicationException("Failed to add project.", ex);
            }
        }

        public async Task DeleteProjectAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to delete project with ID: {id}");
                await _projectRepository.DeleteAsync(id, cancellationToken);
                _logger.LogInformation($"Successfully deleted project with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting project with ID: {id}");
                throw new ApplicationException($"Failed to delete project with ID: {id}", ex);
            }
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting to get all projects.");

                var projects = _mapper.Map<IEnumerable<ProjectDto>>(await _projectRepository.FindAsync(null, cancellationToken));

                _logger.LogInformation($"Successfully retrieved {projects.Count()} projects.");

                return projects;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all projects.");

                throw new ApplicationException("Failed to get all projects.", ex);
            }
        }

        public async Task<ProjectDto> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to get project by ID: {id}");
                var project = _mapper.Map<ProjectDto>(await _projectRepository.FindAsync(x => x.Id == id, cancellationToken));

                if (project == null)
                {
                    _logger.LogWarning($"Project with ID {id} not found.");
                }
                _logger.LogInformation($"Successfully retrieved project with ID: {id}");

                return project;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting project by ID: {id}");
                throw new ApplicationException($"Failed to get project with ID: {id}", ex);
            }
        }

        public async Task<IEnumerable<ProjectDto>> GetProjectsByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to get projects for employee ID: {employeeId}");
                var projects = _mapper.Map<IEnumerable<ProjectDto>>(await _projectRepository.FindAsync(p => p.Employees.Any(e => e.Id == employeeId)));
                _logger.LogInformation($"Successfully retrieved projects for employee ID: {employeeId}");
                return projects;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting projects for employee ID: {employeeId}");
                throw new ApplicationException($"Failed to get projects for employee ID: {employeeId}", ex);
            }
        }

        public async Task UpdateProjectAsync(ProjectDto project, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to update project with ID: {project.Id}");
                if (project == null)
                {
                    throw new ArgumentException("Project cannot be null");
                }

                await _projectRepository.UpsertAsync(_mapper.Map<Project>(project), cancellationToken);

                _logger.LogInformation($"Successfully updated project with ID: {project.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating project with ID: {project.Id}");
                throw new ApplicationException($"Failed to update project with ID: {project.Id}", ex);
            }
        }
    }
}

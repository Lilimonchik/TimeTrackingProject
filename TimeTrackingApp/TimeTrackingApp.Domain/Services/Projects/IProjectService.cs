using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Services.Projects
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync(CancellationToken cancellationToken);
        Task<ProjectDto> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddProjectAsync(ProjectDto project, CancellationToken cancellationToken);
        Task UpdateProjectAsync(ProjectDto project, CancellationToken cancellationToken);
        Task DeleteProjectAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ProjectDto>> GetProjectsByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken);
    }
}

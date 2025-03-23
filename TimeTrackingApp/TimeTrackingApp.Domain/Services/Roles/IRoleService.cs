using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Services.Roles
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync(CancellationToken cancellationToken);
        Task<RoleDto> GetRoleByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddRoleAsync(RoleDto role, CancellationToken cancellationToken);
        Task UpdateRoleAsync(RoleDto role, CancellationToken cancellationToken);
        Task DeleteRoleAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<RoleDto>> GetRolesByNameAsync(string name, CancellationToken cancellationToken);
    }

}

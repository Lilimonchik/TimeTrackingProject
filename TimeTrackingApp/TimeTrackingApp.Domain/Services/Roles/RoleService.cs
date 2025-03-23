using AutoMapper;
using Microsoft.Extensions.Logging;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Domain.Repositories;
using TimeTrackingApp.Domain.Services.Activities;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleService> _logger;
        private readonly IMapper _mapper;
        public RoleService(ILogger<RoleService> logger, IMapper mapper, IRoleRepository roleRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }
        public async Task AddRoleAsync(RoleDto role, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to add new role: {role.Name}");

                if (role == null)
                {
                    _logger.LogError("Received null role.");
                    throw new ArgumentException("Role cannot be null.");
                }

                var roleEntity = _mapper.Map<Role>(role);
                await _roleRepository.AddAsync(roleEntity, cancellationToken);

                _logger.LogInformation($"Successfully added role: {role.Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding role.");
                throw new ApplicationException("Failed to add role.", ex);
            }
        }

        public async Task DeleteRoleAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to delete role with ID: {id}");
                var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

                if (role == null)
                {
                    _logger.LogWarning($"Role with ID: {id} not found.");
                    throw new KeyNotFoundException($"Role with ID: {id} not found.");
                }

                await _roleRepository.DeleteAsync(id, cancellationToken);
                _logger.LogInformation($"Successfully deleted role with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting role with ID: {id}");
                throw new ApplicationException($"Failed to delete role with ID: {id}.", ex);
            }
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting to get all roles.");
                var roles = await _roleRepository.FindAsync(null, cancellationToken);

                if (roles == null || !roles.Any())
                {
                    _logger.LogWarning("No roles found.");
                }

                var result = _mapper.Map<IEnumerable<RoleDto>>(roles);
                _logger.LogInformation($"Successfully retrieved {result.Count()} roles.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all roles.");
                throw new ApplicationException("Failed to get all roles.", ex);
            }
        }

        public async Task<RoleDto> GetRoleByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to get role with ID: {id}");
                var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

                if (role == null)
                {
                    _logger.LogWarning($"Role with ID: {id} not found.");
                    throw new KeyNotFoundException($"Role with ID: {id} not found.");
                }

                var result = _mapper.Map<RoleDto>(role);
                _logger.LogInformation($"Successfully retrieved role with ID: {id}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving role with ID: {id}");
                throw new ApplicationException($"Failed to get role with ID: {id}.", ex);
            }
        }

        public async Task<IEnumerable<RoleDto>> GetRolesByNameAsync(string name, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to get roles with name: {name}");
                var roles = await _roleRepository.FindAsync(x => x.Name == name, cancellationToken);

                if (roles == null || !roles.Any())
                {
                    _logger.LogWarning($"No roles found with name: {name}");
                }

                var result = _mapper.Map<IEnumerable<RoleDto>>(roles);
                _logger.LogInformation($"Successfully retrieved {result.Count()} roles with name: {name}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving roles with name: {name}");
                throw new ApplicationException($"Failed to get roles with name: {name}.", ex);
            }
        }

        public async Task UpdateRoleAsync(RoleDto role, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to update role with ID: {role.Id}");

                if (role == null)
                {
                    _logger.LogError("Received null role.");
                    throw new ArgumentException("Role cannot be null.");
                }

                var roleEntity = _mapper.Map<Role>(role);
                await _roleRepository.UpsertAsync(roleEntity, cancellationToken);

                _logger.LogInformation($"Successfully updated role with ID: {role.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating role with ID: {role.Id}");
                throw new ApplicationException($"Failed to update role with ID: {role.Id}.", ex);
            }
        }
    }
}

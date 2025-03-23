using AutoMapper;
using Microsoft.Extensions.Logging;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Domain.Repositories;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Services.ActivityTypes
{
    public class ActivityTypeService : IActivityTypeService
    {
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly ILogger<ActivityTypeService> _logger;
        private readonly IMapper _mapper;
        public ActivityTypeService(IActivityTypeRepository activityTypeRepository, ILogger<ActivityTypeService> logger, IMapper mapper)
        {
            _activityTypeRepository = activityTypeRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task AddActivityTypeAsync(ActivityTypeDto activity, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to add new activity type: {activity.Name}");
                if (activity == null)
                {
                    _logger.LogError("Received null activity type.");
                    throw new ArgumentException("ActivityType cannot be null.");
                }

                var activityEntity = _mapper.Map<ActivityType>(activity);
                await _activityTypeRepository.AddAsync(activityEntity, cancellationToken);

                _logger.LogInformation($"Successfully added activity type: {activity.Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding activity type.");
                throw new ApplicationException("Failed to add activity type.", ex);
            }
        }

        public async Task DeleteActivityTypeAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to delete activity type with ID: {id}");
                var activityType = await _activityTypeRepository.GetByIdAsync(id, cancellationToken);

                if (activityType == null)
                {
                    _logger.LogWarning($"Activity type with ID: {id} not found.");
                    throw new KeyNotFoundException($"Activity type with ID: {id} not found.");
                }

                await _activityTypeRepository.DeleteAsync(id, cancellationToken);
                _logger.LogInformation($"Successfully deleted activity type with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting activity type with ID: {id}");
                throw new ApplicationException($"Failed to delete activity type with ID: {id}.", ex);
            }
        }

        public async Task<IEnumerable<ActivityTypeDto>> GetActivitiesTypeByNameAsync(string name, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to get activity types with name: {name}");
                var activityTypes = await _activityTypeRepository.FindAsync(x => x.Name == name, cancellationToken);

                if (activityTypes == null || !activityTypes.Any())
                {
                    _logger.LogWarning($"No activity types found with name: {name}");
                }

                var result = _mapper.Map<IEnumerable<ActivityTypeDto>>(activityTypes);
                _logger.LogInformation($"Successfully retrieved {result.Count()} activity types with name: {name}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving activity types with name: {name}");
                throw new ApplicationException($"Failed to get activity types with name: {name}.", ex);
            }
        }

        public async Task<ActivityTypeDto> GetActivityTypeByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to get activity type with ID: {id}");
                var activityType = await _activityTypeRepository.GetByIdAsync(id, cancellationToken);

                if (activityType == null)
                {
                    _logger.LogWarning($"Activity type with ID: {id} not found.");
                    throw new KeyNotFoundException($"Activity type with ID: {id} not found.");
                }

                var result = _mapper.Map<ActivityTypeDto>(activityType);
                _logger.LogInformation($"Successfully retrieved activity type with ID: {id}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving activity type with ID: {id}");
                throw new ApplicationException($"Failed to get activity type with ID: {id}.", ex);
            }
        }

        public async Task<IEnumerable<ActivityTypeDto>> GetAllActivityTypesAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting to get all activity types.");
                var activityTypes = await _activityTypeRepository.FindAsync(null, cancellationToken);

                if (activityTypes == null || !activityTypes.Any())
                {
                    _logger.LogWarning("No activity types found.");
                }

                var result = _mapper.Map<IEnumerable<ActivityTypeDto>>(activityTypes);
                _logger.LogInformation($"Successfully retrieved {result.Count()} activity types.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all activity types.");
                throw new ApplicationException("Failed to get all activity types.", ex);
            }
        }

        public async Task UpdateActivityTypeAsync(ActivityTypeDto activity, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to update activity type with ID: {activity.Id}");
                if (activity == null)
                {
                    _logger.LogError("Received null activity type.");
                    throw new ArgumentException("ActivityType cannot be null.");
                }

                var activityEntity = _mapper.Map<ActivityType>(activity);
                await _activityTypeRepository.UpsertAsync(activityEntity, cancellationToken);

                _logger.LogInformation($"Successfully updated activity type with ID: {activity.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating activity type with ID: {activity.Id}");
                throw new ApplicationException($"Failed to update activity type with ID: {activity.Id}.", ex);
            }
        }
    }
}

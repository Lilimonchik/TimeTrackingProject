using AutoMapper;
using Microsoft.Extensions.Logging;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Domain.Repositories;
using TimeTrackingApp.Domain.Services.Messaging;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Services.Activities
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ILogger<ActivityService> _logger;
        private readonly IMapper _mapper;
        private readonly RabbitMQService _rabbitMQService;
        public ActivityService(ILogger<ActivityService> logger, IMapper mapper, IActivityRepository activityRepository, RabbitMQService rabbitMQService)
        {
            _logger = logger;
            _mapper = mapper;
            _activityRepository = activityRepository;
            _rabbitMQService = rabbitMQService;
        }
        public async Task AddActivityAsync(ActivityDto activity, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting to add a new activity.");
                if (activity == null)
                {
                    throw new ArgumentException("Activity cannot be null");
                }

                await _activityRepository.AddAsync(_mapper.Map<Activity>(activity), cancellationToken);
                _logger.LogInformation($"Successfully added activity with ID: {activity.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new activity.");
                throw new ApplicationException("Failed to add activity.", ex);
            }
        }

        public async Task DeleteActivityAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to delete activity with ID: {id}");
                await _activityRepository.DeleteAsync(id, cancellationToken);
                _logger.LogInformation($"Successfully deleted activity with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting activity with ID: {id}");
                throw new ApplicationException($"Failed to delete activity with ID: {id}", ex);
            }
        }

        public async Task<IEnumerable<ActivityDto>> GetActivitiesByEmployeeAndDateAsync(Guid employeeId, DateTime date, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to get activities for employee {employeeId} on {date:yyyy-MM-dd}.");
                var activities = _mapper.Map<IEnumerable<ActivityDto>>(await _activityRepository.FindAsync(x => x.EmployeeId == employeeId && x.Date == date, cancellationToken));

                if (activities == null || !activities.Any())
                {
                    _logger.LogWarning($"No activities found for employee {employeeId} on {date:yyyy-MM-dd}.");
                }

                _logger.LogInformation($"Successfully retrieved {activities.Count()} activities for employee {employeeId} on {date:yyyy-MM-dd}.");
                return activities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving activities for employee {employeeId} on {date:yyyy-MM-dd}.");
                throw new ApplicationException($"Failed to get activities for employee {employeeId} on {date:yyyy-MM-dd}.", ex);
            }
        }

        public async Task<IEnumerable<ActivityDto>> GetActivitiesByEmployeeAndWeekAsync(Guid employeeId, int weekNumber, CancellationToken cancellationToken)
        {
            try
            {
                var startOfWeek = GetStartOfWeek(weekNumber);
                var endOfWeek = startOfWeek.AddDays(6);

                _logger.LogInformation($"Starting to get activities for employee {employeeId} for week {weekNumber}.");
                var activities = _mapper.Map<IEnumerable<ActivityDto>>(await _activityRepository.FindAsync(x => x.EmployeeId == employeeId && x.Date >= startOfWeek && x.Date <= endOfWeek, cancellationToken));

                if (activities == null || !activities.Any())
                {
                    _logger.LogWarning($"No activities found for employee {employeeId} for week {weekNumber}.");
                }

                _logger.LogInformation($"Successfully retrieved {activities.Count()} activities for employee {employeeId} for week {weekNumber}.");
                return activities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving activities for employee {employeeId} for week {weekNumber}.");
                throw new ApplicationException($"Failed to get activities for employee {employeeId} for week {weekNumber}.", ex);
            }
        }

        public async Task<ActivityDto> GetActivityByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to get activity with ID {id}.");
                var activity = _mapper.Map<IEnumerable<ActivityDto>>(await _activityRepository.FindAsync(x => x.Id == id, cancellationToken));

                if (activity == null)
                {
                    _logger.LogWarning($"Activity with ID {id} not found.");
                }

                _logger.LogInformation($"Successfully retrieved activity with ID {id}.");
                return activity.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving activity with ID {id}.");
                throw new ApplicationException($"Failed to get activity with ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting to get all activities.");
                var activities = _mapper.Map<IEnumerable<ActivityDto>>(await _activityRepository.FindAsync(null, cancellationToken));

                if (activities == null || !activities.Any())
                {
                    _logger.LogWarning("No activities found.");
                }

                _logger.LogInformation($"Successfully retrieved {activities.Count()} activities.");
                return activities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all activities.");
                throw new ApplicationException("Failed to get all activities.", ex);
            }
        }

        public async Task TrackActivityAsync(ActivityDto activity, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to update activity with ID {activity.Id}.");
                if (activity == null)
                {
                    _logger.LogError("Received null activity object.");
                    throw new ArgumentException("Activity cannot be null.");
                }

                await _activityRepository.UpsertAsync(_mapper.Map<Activity>(activity), cancellationToken);
                _logger.LogInformation($"Successfully updated activity with ID {activity.Id}.");

                _rabbitMQService.SendMessage($"New activity: {activity.Id} - {activity.Date}", cancellationToken);

                _logger.LogInformation($"Successfully send activity with ID {activity.Id} using RAbbitMQ.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating activity with ID {activity.Id}.");
                throw new ApplicationException($"Failed to update activity with ID {activity.Id}.", ex);
            }
        }

        public async Task UpdateActivityAsync(ActivityDto activity, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Starting to update activity with ID {activity.Id}.");
                if (activity == null)
                {
                    _logger.LogError("Received null activity object.");
                    throw new ArgumentException("Activity cannot be null.");
                }

                await _activityRepository.UpsertAsync(_mapper.Map<Activity>(activity), cancellationToken);
                _logger.LogInformation($"Successfully updated activity with ID {activity.Id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating activity with ID {activity.Id}.");
                throw new ApplicationException($"Failed to update activity with ID {activity.Id}.", ex);
            }
        }
        private DateTime GetStartOfWeek(int weekNumber)
        {
            var startOfYear = new DateTime(DateTime.Now.Year, 1, 1);
            var daysOffset = (weekNumber - 1) * 7;
            return startOfYear.AddDays(daysOffset);
        }
    }
}

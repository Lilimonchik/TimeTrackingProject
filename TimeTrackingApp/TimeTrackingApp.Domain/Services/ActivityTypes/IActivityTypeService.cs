using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Services.ActivityTypes
{
    public interface IActivityTypeService
    {
        Task<IEnumerable<ActivityTypeDto>> GetAllActivityTypesAsync(CancellationToken cancellationToken);
        Task<ActivityTypeDto> GetActivityTypeByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddActivityTypeAsync(ActivityTypeDto activity, CancellationToken cancellationToken);
        Task UpdateActivityTypeAsync(ActivityTypeDto activity, CancellationToken cancellationToken);
        Task DeleteActivityTypeAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ActivityTypeDto>> GetActivitiesTypeByNameAsync(string name, CancellationToken cancellationToken);
    }
}

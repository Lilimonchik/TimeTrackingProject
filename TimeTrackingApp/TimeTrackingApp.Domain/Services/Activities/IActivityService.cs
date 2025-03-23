using System.Threading;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Domain.Services.Activities
{
    public interface IActivityService
    {
        Task TrackActivityAsync(ActivityDto activityDto, CancellationToken cancellationToken);
        Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync(CancellationToken cancellationToken);
        Task<ActivityDto> GetActivityByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddActivityAsync(ActivityDto activity, CancellationToken cancellationToken);
        Task UpdateActivityAsync(ActivityDto activity, CancellationToken cancellationToken);
        Task DeleteActivityAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ActivityDto>> GetActivitiesByEmployeeAndDateAsync(Guid employeeId, DateTime date, CancellationToken cancellationToken);
        Task<IEnumerable<ActivityDto>> GetActivitiesByEmployeeAndWeekAsync(Guid employeeId, int weekNumber, CancellationToken cancellationToken);
    }
}

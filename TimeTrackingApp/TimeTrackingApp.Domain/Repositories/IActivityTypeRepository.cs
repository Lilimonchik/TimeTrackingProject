using System.Linq.Expressions;
using TimeTrackingApp.Domain.Model;

namespace TimeTrackingApp.Domain.Repositories
{
    public interface IActivityTypeRepository: IRepository<ActivityType>
    {
        public Task<IEnumerable<ActivityType>> FindAsync(
                            Expression<Func<ActivityType, bool>> predicate,
                            CancellationToken cancellationToken = default,
                            bool AsNoTracking = false,
                            bool AsAggregate = true);
    }
}

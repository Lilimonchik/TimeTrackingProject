using System.Linq.Expressions;
using TimeTrackingApp.Domain.Model;

namespace TimeTrackingApp.Domain.Repositories
{
    public interface IProjectRepository: IRepository<Project>
    {
        public Task<IEnumerable<Project>> FindAsync(
                            Expression<Func<Project, bool>> predicate,
                            CancellationToken cancellationToken = default,
                            bool AsNoTracking = false,
                            bool AsAggregate = true);
    }
}

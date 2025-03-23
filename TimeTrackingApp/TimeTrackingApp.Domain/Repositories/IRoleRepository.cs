using System.Linq.Expressions;
using TimeTrackingApp.Domain.Model;
namespace TimeTrackingApp.Domain.Repositories
{
    public interface IRoleRepository: IRepository<Role>
    {
        public Task<IEnumerable<Role>> FindAsync(
                            Expression<Func<Role, bool>> predicate,
                            CancellationToken cancellationToken = default,
                            bool AsNoTracking = false,
                            bool AsAggregate = true);
    }
}

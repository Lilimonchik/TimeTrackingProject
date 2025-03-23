using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingApp.Domain.Model;

namespace TimeTrackingApp.Domain.Repositories
{
    public interface IActivityRepository: IRepository<Activity>
    {
        public Task<IEnumerable<Activity>> FindAsync(
                            Expression<Func<Activity, bool>> predicate,
                            CancellationToken cancellationToken = default,
                            bool AsNoTracking = false,
                            bool AsAggregate = true);
    }
}

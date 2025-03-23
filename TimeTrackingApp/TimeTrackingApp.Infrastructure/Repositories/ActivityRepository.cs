using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Domain.Repositories;
using TimeTrackingApp.Infrastructure.Data;

namespace TimeTrackingApp.Infrastructure.Repositories
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Activity>> FindAsync(Expression<Func<Activity, bool>> predicate, CancellationToken cancellationToken = default, bool AsNoTracking = false, bool AsAggregate = true)
        {
            IQueryable<Activity> result = _context.Activities;

            if (AsAggregate)
                result = result.Include(x => x.Employee)
                               .Include(x => x.Project)
                               .Include(x => x.ActivityType)
                               .Include(x => x.Role);

            if (AsNoTracking)
                result = result.AsNoTracking();

            if (predicate != null)
            {
                result = result.Where(predicate);
            }

            return await result.ToListAsync();
        }
    }
}

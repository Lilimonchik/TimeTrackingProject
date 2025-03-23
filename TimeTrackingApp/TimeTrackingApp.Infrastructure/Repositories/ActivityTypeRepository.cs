using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Domain.Repositories;
using TimeTrackingApp.Infrastructure.Data;

namespace TimeTrackingApp.Infrastructure.Repositories
{
    public class ActivityTypeRepository : Repository<ActivityType>, IActivityTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ActivityType>> FindAsync(Expression<Func<ActivityType, bool>> predicate, CancellationToken cancellationToken = default, bool AsNoTracking = false, bool AsAggregate = true)
        {
            IQueryable<ActivityType> result = _context.ActivityTypes;

            if (AsAggregate)
                result = result.Include(x => x.Activities);


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

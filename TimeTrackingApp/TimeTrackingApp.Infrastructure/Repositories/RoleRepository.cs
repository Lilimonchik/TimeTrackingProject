using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Domain.Repositories;
using TimeTrackingApp.Infrastructure.Data;

namespace TimeTrackingApp.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> FindAsync(Expression<Func<Role, bool>> predicate, CancellationToken cancellationToken = default, bool AsNoTracking = false, bool AsAggregate = true)
        {
            IQueryable<Role> result = _context.Roles;

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

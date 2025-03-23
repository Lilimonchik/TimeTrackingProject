using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Domain.Repositories;
using TimeTrackingApp.Infrastructure.Data;

namespace TimeTrackingApp.Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Project>> FindAsync(Expression<Func<Project, bool>> predicate, CancellationToken cancellationToken = default, bool AsNoTracking = false, bool AsAggregate = true)
        {
            IQueryable<Project> result = _context.Projects;

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

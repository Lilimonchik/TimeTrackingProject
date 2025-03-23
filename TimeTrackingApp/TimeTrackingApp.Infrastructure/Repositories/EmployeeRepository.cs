using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Domain.Repositories;
using TimeTrackingApp.Infrastructure.Data;

namespace TimeTrackingApp.Infrastructure.Repositories
{
    public class EmployeeRepository: Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> FindAsync(Expression<Func<Employee, bool>> predicate, CancellationToken cancellationToken = default, bool AsNoTracking = false, bool AsAggregate = true)
        {
            IQueryable<Employee> result = _context.Employees;

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

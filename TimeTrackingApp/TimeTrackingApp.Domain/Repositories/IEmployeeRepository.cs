using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingApp.Domain.Model;
namespace TimeTrackingApp.Domain.Repositories
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        public Task<IEnumerable<Employee>> FindAsync(
                            Expression<Func<Employee, bool>> predicate,
                            CancellationToken cancellationToken = default,
                            bool AsNoTracking = false,
                            bool AsAggregate = true);
    }
}

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TimeTrackingApp.Domain.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TimeTrackingApp.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpsertAsync(T entity, CancellationToken cancellationToken)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                var existingEntity = await _dbSet
                    .FindAsync(new object[] { entry.OriginalValues["Id"] }, cancellationToken);

                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).State = EntityState.Modified;
                    _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                }
                else
                {
                    _dbSet.Add(entity);
                }
            }
            else
            {
                _dbSet.Update(entity);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

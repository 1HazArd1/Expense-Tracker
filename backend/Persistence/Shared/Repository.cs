using Expense.Tracker.Application.Interface.Persistence;
using Expense.Tracker.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Expense.Tracker.Persistence.Shared
{
    public class Repository<T>(IDatabaseContext database) : IRepository<T>
        where T : class, IEntity
    {
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await database.Set<T>().AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await database.Set<T>().AddRangeAsync(entities, cancellationToken);
        }

        public void AttachEntity(T entity)
        {
             database.Set<T>().Attach(entity);
        }

        public IQueryable<T> GetAll()
        {
            return database.Set<T>();
        }

        public IQueryable<T> GetAllAsNoTracking()
        {
            return GetAll().AsNoTracking();
        }

        public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await database.Set<T>().FindAsync(id , cancellationToken);
        }

        public void Remove(T entity)
        {
            database.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            database.Set<T>().Update(entity);
        }
    }
}

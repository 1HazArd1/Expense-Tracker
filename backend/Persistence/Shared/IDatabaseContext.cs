using Expense.Tracker.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Expense.Tracker.Persistence.Shared
{
    public interface IDatabaseContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;
        
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }

    public interface IOLTPDatabaseContext : IDatabaseContext 
    {
    }
}

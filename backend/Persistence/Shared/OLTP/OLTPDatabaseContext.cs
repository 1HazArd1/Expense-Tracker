using Expense.Tracker.Domain.Common;
using Expense.Tracker.Persistence.Expense;
using Expense.Tracker.Persistence.User;
using Microsoft.EntityFrameworkCore;

namespace Expense.Tracker.Persistence.Shared.OLTP
{
    public class OLTPDatabaseContext(DbContextOptions<OLTPDatabaseContext> options) : DbContext(options), IOLTPDatabaseContext
    {

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity
        {
            return base.Set<TEntity>();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ExpenseCalendarsConfiguration());
            modelBuilder.ApplyConfiguration(new UserMasterConfiguration());
        }
    }
}

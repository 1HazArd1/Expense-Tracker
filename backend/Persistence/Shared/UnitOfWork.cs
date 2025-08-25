using Expense.Tracker.Application.Interface.Persistence;

namespace Expense.Tracker.Persistence.Shared
{
    public abstract class UnitOfWork(IDatabaseContext database)
    {
        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await database.SaveAsync(cancellationToken);
        }
    }
    public class OLTPUnitOfWork(IOLTPDatabaseContext database) : UnitOfWork(database), IOLTPUnitOfWork
    {
    }
}

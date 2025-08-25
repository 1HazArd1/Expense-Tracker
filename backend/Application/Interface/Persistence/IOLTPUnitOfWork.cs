namespace Expense.Tracker.Application.Interface.Persistence
{
    public interface IOLTPUnitOfWork
    {
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}

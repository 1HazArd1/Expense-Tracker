namespace Expense.Tracker.Application.Interface.Persistence
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetAllAsNoTracking();

        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        void Remove(T entity);

        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        void AttachEntity(T entity);

        void Update(T entity);

        Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    }
}

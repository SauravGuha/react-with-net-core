

using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IQueryRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? condition, CancellationToken token, params string[] includeProperties);

        Task<T?> GetById(Guid id, CancellationToken token);

    }
}
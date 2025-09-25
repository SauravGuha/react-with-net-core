

namespace Domain.Repositories
{
    public interface ICommandRepository<T>
    {
        Task<T> CreateAsync(T entity, CancellationToken token);

        Task<T> UpdateAsync(T entity, CancellationToken token);

        Task<int> DeleteAsync(T Entity, CancellationToken token);

    }
}
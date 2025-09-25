

namespace Domain.Repositories
{
    public interface ICommandRepository<T>
    {
        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<int> DeleteAsync(T Entity);

    }
}
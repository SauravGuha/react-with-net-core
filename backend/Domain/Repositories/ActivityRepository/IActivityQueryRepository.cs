

using System.Linq.Expressions;
using Domain.Models;

namespace Domain.Repositories.ActivityRepository
{
    public interface IActivityQueryRepository : IQueryRepository<Activity>
    {
        Task<IQueryable<Activity>> GetAllAsync(Expression<Func<Activity, bool>> condition, CancellationToken token);
    }
}
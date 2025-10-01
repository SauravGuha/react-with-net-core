
using System.Linq.Expressions;
using Domain.Models;
using Domain.Repositories.ActivityRepository;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository.ActivityRepository
{
    public class ActivityQueryRepository : IActivityQueryRepository
    {
        private readonly ActivityDbContext activityDbContext;
        public ActivityQueryRepository(ActivityDbContext activityDbContext)
        {
            this.activityDbContext = activityDbContext;

        }

        public async Task<IEnumerable<Activity>> GetAllAsync(Expression<Func<Activity, bool>> condition, CancellationToken token,
         params string[] includeProperties)
        {
            var query = activityDbContext.Activities.AsQueryable();

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            IEnumerable<Activity> result = await query.Where(condition).ToListAsync(token);

            return result;
        }

        public async Task<Activity?> GetById(Guid id, CancellationToken token, params string[] includeProperties)
        {
            var query = activityDbContext.Activities.AsQueryable();

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return await query.FirstOrDefaultAsync(e => e.Id == id, token);
        }

        public Task<IQueryable<Activity>> GetAllAsync(Expression<Func<Activity, bool>> condition, CancellationToken token)
        {
            return Task.FromResult(this.activityDbContext.Activities.Where(condition));
        }

    }
}
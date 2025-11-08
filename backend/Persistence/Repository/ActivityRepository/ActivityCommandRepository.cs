

using Domain.Models;
using Domain.Repositories.ActivityRepository;

namespace Persistence.Repository.ActivityRepository
{
    public class ActivityCommandRepository : IActivityCommandRepository
    {
        private readonly ActivityDbContext activityDbContext;
        public ActivityCommandRepository(ActivityDbContext activityDbContext)
        {
            this.activityDbContext = activityDbContext;

        }

        public async Task<Activity> CreateAsync(Activity entity, CancellationToken token)
        {
            activityDbContext.Activities.Add(entity);
            await activityDbContext.SaveChangesAsync(token);
            return entity;
        }

        public async Task<int> DeleteAsync(Activity entity, CancellationToken token)
        {
            activityDbContext.Activities.Remove(entity);
            return await activityDbContext.SaveChangesAsync(token);
        }

        public async Task<Activity> UpdateAsync(Activity entity, CancellationToken token)
        {
            activityDbContext.Activities.Update(entity);
            await activityDbContext.SaveChangesAsync(token);
            return entity;
        }
    }
}
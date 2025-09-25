

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
            var activity = activityDbContext.Activities.FirstOrDefault(e => e.Title.ToLower() == entity.Title.ToLower()
            && e.EventDate == entity.EventDate && e.Category.ToLower() == entity.Category.ToLower());
            if (activity == null)
            {
                activityDbContext.Activities.Add(entity);
                await activityDbContext.SaveChangesAsync(token);
                return entity;
            }
            else
                throw new InvalidOperationException("Cannot create same event on the same date");
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
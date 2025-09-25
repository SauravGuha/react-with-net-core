

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

        public async Task<Activity> CreateAsync(Activity entity)
        {
            var activity = activityDbContext.Activities.FirstOrDefault(e => e.Title.ToLower() == entity.Title.ToLower()
            && e.EventDate == entity.EventDate && e.Category.ToLower() == entity.Category.ToLower());
            if (activity == null)
            {
                activityDbContext.Activities.Add(entity);
                await activityDbContext.SaveChangesAsync();
                return entity;
            }
            else
                throw new InvalidOperationException("Cannot create same event on the same date");
        }

        public Task<int> DeleteAsync(Activity Entity)
        {
            throw new NotImplementedException();
        }

        public Task<Activity> UpdateAsync(Activity entity)
        {
            throw new NotImplementedException();
        }
    }
}
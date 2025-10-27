
using Domain.Models;
using Domain.Repositories.UserRepository;

namespace Persistence.Repository.UserFollowingRepository
{
    public class UserFollowingCommandRepository : IUserFollowingCommandRepository
    {
        private readonly ActivityDbContext activityDbContext;
        public UserFollowingCommandRepository(ActivityDbContext activityDbContext)
        {
            this.activityDbContext = activityDbContext;

        }

        public async Task<UserFollowing> CreateAsync(UserFollowing entity, CancellationToken token)
        {
            activityDbContext.UserFollowings.Add(entity);
            await activityDbContext.SaveChangesAsync(token);
            return entity;
        }

        public async Task<int> DeleteAsync(UserFollowing entity, CancellationToken token)
        {
            activityDbContext.UserFollowings.Remove(entity);
            return await activityDbContext.SaveChangesAsync(token);
        }

        public Task<UserFollowing> UpdateAsync(UserFollowing entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}

using Domain.Models;
using Domain.Repositories.UserRespository;

namespace Persistence.Repository.UserRepository
{
    public class UserCommandRepository : IUserCommandRepository
    {
        private readonly ActivityDbContext activityDbContext;
        public UserCommandRepository(ActivityDbContext activityDbContext)
        {
            this.activityDbContext = activityDbContext;

        }

        public async Task<User> CreateAsync(User entity, CancellationToken token)
        {
            this.activityDbContext.Users.Add(entity);
            await this.activityDbContext.SaveChangesAsync(token);
            return entity;
        }

        public async Task<int> DeleteAsync(User entity, CancellationToken token)
        {
            this.activityDbContext.Users.Remove(entity);
            return await this.activityDbContext.SaveChangesAsync(token);
        }

        public async Task<User> UpdateAsync(User entity, CancellationToken token)
        {
            this.activityDbContext.Users.Update(entity);
            await this.activityDbContext.SaveChangesAsync(token);
            return entity;
        }
    }
}
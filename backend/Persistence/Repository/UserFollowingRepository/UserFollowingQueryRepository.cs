

using System.Linq.Expressions;
using Domain.Models;
using Domain.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository.UserFollowingRepository
{
    public class UserFollowingQueryRepository : IUserFollowingQueryRepository
    {
        private readonly ActivityDbContext activityDbContext;
        public UserFollowingQueryRepository(ActivityDbContext activityDbContext)
        {
            this.activityDbContext = activityDbContext;

        }
        public async Task<IEnumerable<UserFollowing>> GetAllAsync(Expression<Func<UserFollowing, bool>>? condition,
        CancellationToken token, params string[] includeProperties)
        {
            var query = activityDbContext.UserFollowings.AsQueryable();

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            IEnumerable<UserFollowing> result = await query.Where(condition).ToListAsync(token);

            return result;
        }

        public async Task<UserFollowing?> GetById(Guid id, CancellationToken token, params string[] includeProperties)
        {
            var query = activityDbContext.UserFollowings.AsQueryable();

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return await query.FirstOrDefaultAsync(e => e.Id == id, token);
        }
    }
}
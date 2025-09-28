

using System.Linq.Expressions;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository.UserRepository
{
    public class UserQueryRepository : IQueryRepository<User>
    {
        private readonly ActivityDbContext activityDbContext;
        public UserQueryRepository(ActivityDbContext activityDbContext)
        {
            this.activityDbContext = activityDbContext;

        }
        public async Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>>? condition, CancellationToken token,
        params string[] includeProperties)
        {
            var query = activityDbContext.Users.AsQueryable();

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }

            if (condition != null)
                return await query.Where(condition).ToListAsync(token);
            else
                return await query.ToListAsync(token);
        }

        public async Task<User?> GetById(Guid id, CancellationToken token)
        {
            return await activityDbContext.Users.FindAsync(id, token);
        }
    }
}
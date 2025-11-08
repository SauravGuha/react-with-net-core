
using System.Linq.Expressions;
using Domain.Models;
using Domain.Repositories.PhotoRepository;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository.CommentRepository
{
    public class CommentQueryRepository : ICommentQueryRepository
    {
        private readonly ActivityDbContext activityDbContext;
        public CommentQueryRepository(ActivityDbContext activityDbContext)
        {
            this.activityDbContext = activityDbContext;

        }
        public async Task<IEnumerable<Comment>> GetAllAsync(Expression<Func<Comment, bool>>? condition,
        CancellationToken token, params string[] includeProperties)
        {
            var query = activityDbContext.Comments.AsQueryable();

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }

            IEnumerable<Comment>? result = null;
            if (condition != null)
                result = await query.Where(condition).ToListAsync(token);
            else
                result = await query.ToListAsync(token);

            return result;
        }

        public async Task<Comment?> GetById(Guid id, CancellationToken token, params string[] includeProperties)
        {
            var query = activityDbContext.Comments.AsQueryable();

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return await query.FirstOrDefaultAsync(e => e.Id == id, token);
        }
    }
}
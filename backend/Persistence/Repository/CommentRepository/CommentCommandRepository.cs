

using Domain.Models;
using Domain.Repositories.PhotoRepository;

namespace Persistence.Repository.CommentRepository
{
    public class CommentCommandRepository : ICommentCommandRepository
    {
        private readonly ActivityDbContext activityDbContext;
        public CommentCommandRepository(ActivityDbContext activityDbContext)
        {
            this.activityDbContext = activityDbContext;

        }
        public async Task<Comment> CreateAsync(Comment entity, CancellationToken token)
        {
            activityDbContext.Comments.Add(entity);
            await activityDbContext.SaveChangesAsync(token);
            return entity;
        }

        public async Task<int> DeleteAsync(Comment entity, CancellationToken token)
        {
            activityDbContext.Comments.Remove(entity);
            return await activityDbContext.SaveChangesAsync(token);
        }

        public async Task<Comment> UpdateAsync(Comment entity, CancellationToken token)
        {
            activityDbContext.Comments.Update(entity);
            await activityDbContext.SaveChangesAsync(token);
            return entity;
        }
    }
}
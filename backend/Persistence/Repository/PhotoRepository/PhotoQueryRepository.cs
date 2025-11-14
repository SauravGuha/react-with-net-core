
using System.Linq.Expressions;
using Domain.Models;
using Domain.Repositories.PhotoRepository;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository.ActivityRepository
{
    public class PhotoQueryRepository : IPhotoQueryRepository
    {
        private readonly ActivityDbContext activityDbContext;
        public PhotoQueryRepository(ActivityDbContext activityDbContext)
        {
            this.activityDbContext = activityDbContext;

        }

        public async Task<IEnumerable<Photo>> GetAllAsync(Expression<Func<Photo, bool>>? condition, CancellationToken token,
         params string[] includeProperties)
        {
            var query = activityDbContext.Photos.AsQueryable();

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            if (condition != null)
                return await query.Where(condition).ToListAsync(token);
            else
                return await query.ToListAsync(token);
        }

        public async Task<Photo?> GetById(Guid id, CancellationToken token, params string[] includeProperties)
        {
            var query = activityDbContext.Photos.AsQueryable();

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return await query.FirstOrDefaultAsync(e => e.Id == id, token);
        }

    }
}
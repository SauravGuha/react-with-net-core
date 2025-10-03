

using Domain.Models;
using Domain.Repositories.PhotoRepository;


namespace Persistence.Repository.PhotoRepository
{
    public class PhotoCommandRepository : IPhotoCommandRepository
    {
        private readonly ActivityDbContext activityDbContext;
        public PhotoCommandRepository(ActivityDbContext activityDbContext)
        {
            this.activityDbContext = activityDbContext;

        }

        public async Task<Photo> CreateAsync(Photo entity, CancellationToken token)
        {
            activityDbContext.Photos.Add(entity);
            await activityDbContext.SaveChangesAsync(token);
            return entity;
        }

        public async Task<int> DeleteAsync(Photo entity, CancellationToken token)
        {
            activityDbContext.Photos.Remove(entity);
            return await activityDbContext.SaveChangesAsync(token);
        }

        public async Task<Photo> UpdateAsync(Photo entity, CancellationToken token)
        {
            activityDbContext.Photos.Update(entity);
            await activityDbContext.SaveChangesAsync(token);
            return entity;
        }
    }
}
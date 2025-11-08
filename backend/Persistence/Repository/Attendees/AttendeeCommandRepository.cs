


using Domain.Repositories.AttendeeRespository;

namespace Persistence.Repository.AttendeesRepo
{
    public class AttendeeCommandRepository : IAttendeeCommandRepository
    {

        private readonly ActivityDbContext activityDbContext;
        public AttendeeCommandRepository(ActivityDbContext activityDbContext)
        {
            this.activityDbContext = activityDbContext;

        }

        public async Task<Domain.Models.Attendees> CreateAsync(Domain.Models.Attendees entity, CancellationToken token)
        {
            this.activityDbContext.Attendees.Add(entity);
            await this.activityDbContext.SaveChangesAsync(token);
            return entity;
        }

        public Task<int> DeleteAsync(Domain.Models.Attendees entity, CancellationToken token)
        {
            this.activityDbContext.Attendees.Remove(entity);
            return this.activityDbContext.SaveChangesAsync(token);
        }

        public async Task<Domain.Models.Attendees> UpdateAsync(Domain.Models.Attendees entity, CancellationToken token)
        {
            this.activityDbContext.Attendees.Update(entity);
            await this.activityDbContext.SaveChangesAsync(token);
            return entity;
        }
    }
}
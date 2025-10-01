

using System.Linq.Expressions;
using Domain.Repositories.AttendeeRespository;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository.AttendeesRepo
{
    public class AttendeeQueryRepository : IAttendeeQueryRepository
    {
        private readonly ActivityDbContext activityDbContext;
        public AttendeeQueryRepository(ActivityDbContext activityDbContext)
        {
            this.activityDbContext = activityDbContext;

        }
        public async Task<IEnumerable<Domain.Models.Attendees>> GetAllAsync(Expression<Func<Domain.Models.Attendees, bool>> condition,
        CancellationToken token, params string[] includeProperties)
        {
            var query = activityDbContext.Attendees.AsQueryable();

            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            IEnumerable<Domain.Models.Attendees> result = await query.Where(condition).ToListAsync(token);

            return result;
        }

        public Task<Domain.Models.Attendees?> GetById(Guid id, CancellationToken token, params string[] includeProperties)
        {
            throw new NotImplementedException();
        }
    }
}
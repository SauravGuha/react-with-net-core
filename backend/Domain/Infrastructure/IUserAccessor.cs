
using Domain.Models;

namespace Domain.Infrastructure
{
    public interface IUserAccessor
    {
        Task<User> GetUserAsync();

        string GetUserId();
    }
}
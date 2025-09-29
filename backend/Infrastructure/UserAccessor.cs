
using Domain.Infrastructure;

namespace Infrastructure;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor httpAccessor;
    public UserAccessor(IHttpAccessor httpAccessor)
    {
        this.httpAccessor = httpAccessor;

    }
    public async Task<User> GetUserAsync()
    {
        return null;
    }
}

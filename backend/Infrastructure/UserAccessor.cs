
using Domain.Infrastructure;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor httpAccessor;
    private readonly UserManager<User> userManager;

    public UserAccessor(IHttpContextAccessor httpAccessor, UserManager<User> userManager)
    {
        this.httpAccessor = httpAccessor;
        this.userManager = userManager;
    }
    public async Task<User> GetUserAsync()
    {
        var user = await userManager.GetUserAsync(this.httpAccessor.HttpContext.User);
        return user!;
    }

    public string GetUserId()
    {
        return this.userManager.GetUserId(this.httpAccessor.HttpContext.User)!;
    }
}

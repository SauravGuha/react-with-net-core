
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

    /// <summary>
    /// Gets the login user details
    /// </summary>
    /// <returns></returns>
    public string GetUserId()
    {
        return this.userManager.GetUserId(this.httpAccessor.HttpContext.User)!;
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        return user!;
    }

    public async Task<int> UpdateUser(User user)
    {
        var result = await userManager.UpdateAsync(user);
        if (result.Succeeded)
            return 1;
        else
            return 0;
    }
}

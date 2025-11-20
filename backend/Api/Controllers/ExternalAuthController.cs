

using System.Security.Claims;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ExternalAuthController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManger;
        public ExternalAuthController(SignInManager<User> signInManger, UserManager<User> userManager)
        {
            this.signInManger = signInManger;
            this.userManager = userManager;

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromQuery] string provider, [FromQuery] string returnUrl = "/")
        {
            var providerProps = signInManger.ConfigureExternalAuthenticationProperties(provider, returnUrl);
            return Challenge(providerProps, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Callback()
        {
            var info = await signInManger.GetExternalLoginInfoAsync();
            if (info == null)
                throw new Exception("No external login provider found");

            var userData = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (userData == null)
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email == null)
                    throw new Exception("No external login provider found");
                else
                {
                    userData = new User
                    {
                        Email = email,
                        UserName = email,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(userData);
                }
            }

            // Add the login mapping (so next time login is automatic)
            await userManager.AddLoginAsync(userData, info);
            // Sign the user in with cookie
            await signInManger.SignInAsync(userData, isPersistent: false);
            return Redirect("/");
        }

    }
}
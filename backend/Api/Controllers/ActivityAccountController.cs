

using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ActivityAccountController : BaseController
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public ActivityAccountController(SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestViewModel request)
        {
            var result = await signInManager.PasswordSignInAsync(
                request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
                return NotFound("Invalid username or password");

            return Ok("Logged in successfully");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok("Successfully logged out");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationViewModel viewModel)
        {
            var userDetails = await this.userManager.FindByEmailAsync(viewModel.Email);
            if (userDetails == null)
            {
                await userManager.CreateAsync(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = viewModel.Email,
                    Email = viewModel.Email,
                    Bio = viewModel.Bio,
                    DisplayName = viewModel.DisplayName,
                    ImageUrl = viewModel.ImageUrl
                }, viewModel.Password);
                return Ok("User created");
            }
            else
            {
                return Conflict("Email already exists");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> UserDetail()
        {
            if (this.HttpContext.User.Identity != null
            && this.HttpContext.User.Identity.IsAuthenticated)
            {
                var userDetails = await userManager.GetUserAsync(this.HttpContext.User);
                return Ok(this.mapper.Map<UserViewModel>(userDetails));
            }
            else
            {
                return NoContent();
            }
        }

    }
}

using Application.ViewModels;
using AutoMapper;
using Domain.Infrastructure;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    public class ActivityAccountController : BaseController
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IEmailSenderService emailSenderService;
        private readonly ILogger<ActivityAccountController> logger;

        public ActivityAccountController(SignInManager<User> signInManager,
        UserManager<User> userManager,
        IMapper mapper, IEmailSenderService emailSenderService,
        ILogger<ActivityAccountController> logger)
        {
            this.mapper = mapper;
            this.emailSenderService = emailSenderService;
            this.logger = logger;
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
                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = viewModel.Email,
                    Email = viewModel.Email,
                    Bio = viewModel.Bio,
                    DisplayName = viewModel.DisplayName,
                    ImageUrl = viewModel.ImageUrl
                };
                var identityResult = await userManager.CreateAsync(user, viewModel.Password);
                if (identityResult.Succeeded)
                {
                    await emailSenderService.SendConfirmationEmail(user.Email, user.DisplayName ?? "");
                    return Ok("User created");
                }
                else
                    return this.Problem(string.Join("\n", identityResult.Errors));
            }
            else
            {
                return Conflict("Email already exists");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAddress([FromQuery] string userId,
        [FromQuery] string code, [FromQuery] string emailId)
        {
            return Ok();
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
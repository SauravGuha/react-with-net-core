

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ExternalAuthController : BaseController
    {
        public ExternalAuthController()
        {

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GitHubSignIn([FromQuery] string? returnUrl)
        {
            return Ok();
        }
    }
}
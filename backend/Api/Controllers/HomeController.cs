using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to Reactivities api.");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to Reactivities api.");
        }
    }
}

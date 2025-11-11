

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    public class FallBackController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public FallBackController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var wwwrootPath = this.webHostEnvironment.WebRootPath;
            return PhysicalFile(Path.Combine(wwwrootPath, "index.html"), "text/HTML");
        }
    }
}
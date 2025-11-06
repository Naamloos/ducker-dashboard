using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Naamloos.Ducker.Controllers
{
    [Route("containers")]
    public class ContainersController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Inertia.Render("construction");
        }

        [HttpGet("new")]
        public async Task<IActionResult> New()
        {
            return Inertia.Render("construction");
        }
    }
}

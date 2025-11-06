using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Naamloos.Ducker.Controllers
{
    [Route("dashboard")]
    public class DashboardController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Inertia.Render("construction");
        }

        [HttpGet("analytics")]
        public async Task<IActionResult> Analytics()
        {
            return Inertia.Render("construction");
        }
    }
}

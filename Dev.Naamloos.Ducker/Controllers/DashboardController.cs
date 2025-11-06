using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Naamloos.Ducker.Controllers
{
    [Route("dashboard")]
    [Authorize]
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

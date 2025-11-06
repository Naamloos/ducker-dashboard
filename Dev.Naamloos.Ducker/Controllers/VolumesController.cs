using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Naamloos.Ducker.Controllers
{
    [Route("volumes")]
    public class VolumesController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Inertia.Render("construction");
        }

        [HttpGet("new")]
        public async Task<IActionResult> Create()
        {
            return Inertia.Render("construction");
        }
    }
}

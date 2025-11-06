using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Naamloos.Ducker.Controllers
{
    [Route("images")]
    public class ImagesController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Inertia.Render("construction");
        }

        [HttpGet("pull")]
        public async Task<IActionResult> PullImage()
        {
            return Inertia.Render("construction");
        }

        [HttpGet("history")]
        public async Task<IActionResult> BuildHistory()
        {
            return Inertia.Render("construction");
        }
    }
}

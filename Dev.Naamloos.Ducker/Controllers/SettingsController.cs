using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Naamloos.Ducker.Controllers
{
    [Route("settings")]
    public class SettingsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> General()
        {
            return Inertia.Render("construction");
        }

        [HttpGet("docker")]
        public async Task<IActionResult> Docker()
        {
            return Inertia.Render("construction");
        }
    }
}

using Dev.Naamloos.Ducker.Services;
using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Naamloos.Ducker.Controllers
{
    [Route("/")]
    [Authorize]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index([FromServices]IDockerService docker)
        {
            return Inertia.Render("index", new
            {
                containers = await docker.ListContainersAsync()
            });
        }
    }
}

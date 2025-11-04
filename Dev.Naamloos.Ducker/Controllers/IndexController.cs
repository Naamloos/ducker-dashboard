using Dev.Naamloos.Ducker.Services;
using InertiaCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Naamloos.Ducker.Controllers
{
    [Route("/")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index(IDockerService docker)
        {
            return Inertia.Render("index", new
            {
                containers = await docker.ListContainersAsync()
            });
        }
    }
}

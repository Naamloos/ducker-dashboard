using Dev.Naamloos.Ducker.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Naamloos.Ducker.Controllers.Api
{
    [ApiController]
    [Route("api")]
    public class ApiIndexController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return new JsonResult(new
            {
                name = "Ducker API",
                status = "running",
            });
        }

        [HttpGet("check")]
        [ApiAuthorize]
        public IActionResult Check()
        {
            return new JsonResult(new
            {
                status = "ok",
            });
        }
    }
}

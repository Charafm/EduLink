using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace SchoolSaas.Web.Common.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api")]
    public class AppLifetimeController : ControllerBase
    {
        private readonly IHostApplicationLifetime _applicationLifetime;

        public AppLifetimeController(IHostApplicationLifetime applicationLifetime)
        {
            _applicationLifetime = applicationLifetime;
        }

        [HttpPost("reload")]
        [RestrictToLocalhost]
        public ActionResult Reload()
        {
            _applicationLifetime.StopApplication();
            return Ok();
        }
    }
}
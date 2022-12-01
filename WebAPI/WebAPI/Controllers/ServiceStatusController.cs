using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class ServiceStatusController : ControllerBase
    {
        [HttpGet("readines")]
        public IActionResult GetReadiness()
        {
            return new OkResult();
        }

        [HttpGet("liveness")]
        public IActionResult GetLiveness()
        {
            return new OkResult();
        }
    }
}

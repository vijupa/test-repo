using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class ServiceStatusController : ControllerBase
    {
        private readonly ILogger<ServiceStatusController> _logger;

        public ServiceStatusController(ILogger<ServiceStatusController> logger)
        {
            _logger = logger;
        }

        [HttpGet("readines")]
        public IActionResult GetReadiness()
        {
            _logger.LogInformation("GetReadines");
            return new OkResult();
        }

        [HttpGet("liveness")]
        public IActionResult GetLiveness()
        {
            _logger.LogInformation("GetLiveness");
            return new OkResult();
        }
    }
}

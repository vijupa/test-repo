using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Controller]
    [Route("/")]
    public class IndexController : ControllerBase
    {
        private readonly ILogger<IndexController> _logger;

        public IndexController(ILogger<IndexController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            _logger.LogInformation("INDEX");
            return new OkResult();
        }
    }
}

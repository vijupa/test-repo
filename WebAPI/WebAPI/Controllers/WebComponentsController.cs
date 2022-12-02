using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("components")]
    public class WebComponentsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<WebComponentsController> _logger;

        public WebComponentsController(IConfiguration configuration,
            ILogger<WebComponentsController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        [Route("example.js")]
        public async Task<IActionResult> GetExample()
        {
            _logger.LogInformation("GetExample");
            var text = await System.IO.File
                .ReadAllTextAsync("./ScriptFiles/components/example.js");
            var apiUrl = _configuration["API_URL"];
            
            if (apiUrl == null) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            text = text.Replace("#--API_URL--#", apiUrl);

            _logger.LogInformation("GetExample exit");
            return File(Encoding.UTF8.GetBytes(text), "application/javascript");
        }

        [HttpGet]
        [Route("signalrexample.js")]
        public async Task<IActionResult> GetSignalRExample()
        {
            var text = await System.IO.File
                .ReadAllTextAsync("./ScriptFiles/components/signalrexample.js");
            var apiUrl = _configuration["API_URL"];

            if (apiUrl == null) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            text = text.Replace("#--API_URL--#", apiUrl);

            return File(Encoding.UTF8.GetBytes(text), "application/javascript");
        }

        [HttpGet]
        [Route("meeting.js")]
        public async Task<IActionResult> GetMeeting()
        {
            _logger.LogInformation("GetMeeting");
            var text = await System.IO.File
                .ReadAllTextAsync("./ScriptFiles/components/meeting.js");
            var apiUrl = _configuration["API_URL"];

            if (apiUrl == null) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            text = text.Replace("#--API_URL--#", apiUrl);

            _logger.LogInformation("GetMeeting exit");
            return File(Encoding.UTF8.GetBytes(text), "application/javascript");
        }

        [HttpGet]
        [Route("decision.js")]
        public async Task<IActionResult> GetDecision()
        {
            var text = await System.IO.File
                .ReadAllTextAsync("./ScriptFiles/components/decision.js");
            var apiUrl = _configuration["API_URL"];

            if (apiUrl == null) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            text = text.Replace("#--API_URL--#", apiUrl);
            
            return File(Encoding.UTF8.GetBytes(text), "application/javascript");
        }
    }
}
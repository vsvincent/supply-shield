using Gateway.Extensions;
using Gateway.Models;
using Gateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncidentController : ControllerBase
    {
        private readonly ILogger<IncidentController> _logger;
        private readonly CommunicationService communicationService = new CommunicationService();
        public IncidentController(ILogger<IncidentController> logger)
        {
            _logger = logger;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            FirebaseUser user = HttpContext.GetFirebaseUser();

            return Ok(user);
        }
        [HttpGet("test")]
        public async Task<IActionResult> GetTest()
        {
            return Ok(await communicationService.GetItem<String>("wow"));
        }
    }
}

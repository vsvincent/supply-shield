using Gateway.Extensions;
using Gateway.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FirebaseController : ControllerBase
    {
        private readonly ILogger<FirebaseController> _logger;

        public FirebaseController(ILogger<FirebaseController> logger)
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
        [Authorize]
        [HttpPost]
        public IActionResult Add()
        {
            return Ok("This should be some cool text.");
        }
    }
}
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<UserController> _logger;
        private readonly string _userServiceUrl = "https://user-service-cmlmuykhqq-lm.a.run.app";
        public UserController(ILogger<UserController> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> Set()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                string requestBody = await reader.ReadToEndAsync();
                return Ok(await _clientService.Post(_userServiceUrl, requestBody));
            }
        }
    }
}
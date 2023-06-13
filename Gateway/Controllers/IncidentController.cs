using Gateway.Extensions;
using Gateway.Models;
using Gateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

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
        [HttpPost]
        public async Task<IActionResult> Add()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                string requestBody = await reader.ReadToEndAsync();
                JObject incident = JsonConvert.DeserializeObject<JObject>(requestBody);
                return Ok(await communicationService.GetItem<JObject>(incident));
            }
        }
    }
}

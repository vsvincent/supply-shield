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
        private readonly IClientService _clientService;
        private readonly string _incidentServiceUrl = "https://incident-service-cmlmuykhqq-lm.a.run.app/";
        public IncidentController(ILogger<IncidentController> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(string organizationId)
        {
            return Ok(await _clientService.Get($"{_incidentServiceUrl}/incident?organizationId={organizationId}"));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                string requestBody = await reader.ReadToEndAsync();
                _logger.LogInformation("Processing incident declaration request: " + requestBody);
                JObject incident = JsonConvert.DeserializeObject<JObject>(requestBody);
                communicationService.GetItem<JObject>(incident);
                return Ok("Incident created: " + incident["Description"]);
            }
        }
    }
}

using IncidentService.Models;
using IncidentService.Service;
using Microsoft.AspNetCore.Mvc;

namespace IncidentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncidentController : ControllerBase
    {

        private readonly ILogger<IncidentController> _logger;
        private readonly IIncidentService _incidentService;

        public IncidentController(ILogger<IncidentController> logger, IIncidentService incidentService)
        {
            _logger = logger;
            _incidentService = incidentService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _incidentService.GetAll());
        }
        [HttpPost]
        public async Task<IActionResult> AddIncident([FromBody] Incident incident)
        {
            _incidentService.Add(incident);
            return Ok($"Created incident {incident.Description} with type {incident.Type}.");
        }
    }
}
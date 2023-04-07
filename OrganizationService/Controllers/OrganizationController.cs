using Amazon.QLDB.Driver;
using Amazon.QLDBSession;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Models;
using OrganizationService.Repository;
using OrganizationService.Services;

namespace OrganizationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : ControllerBase
    {

        private readonly ILogger<OrganizationController> _logger;
        private readonly IOrganizationService _organizationService;

        public OrganizationController(ILogger<OrganizationController> logger, IOrganizationService organizationService)
        {
            _logger = logger;
            _organizationService = organizationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _organizationService.GetAll());
        }
        [HttpPost]
        public async Task<IActionResult> AddOrganization([FromBody] Organization organization)
        {
            _organizationService.Add(organization);
            return Ok($"Created organization {organization.Name} with code {organization.Code}.");
        }
    }
}
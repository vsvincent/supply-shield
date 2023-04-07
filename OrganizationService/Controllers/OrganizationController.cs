using Amazon.QLDB.Driver;
using Amazon.QLDBSession;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Google.Cloud.SecretManager.V1;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Models;
using OrganizationService.Repository;
using OrganizationService.Services;
using OrganizationService.Utils;

namespace OrganizationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : ControllerBase
    {

        private readonly ILogger<OrganizationController> _logger;
        private readonly IOrganizationService _organizationService;
        private readonly IGoogleSecretManager _secretManager;

        public OrganizationController(ILogger<OrganizationController> logger, IOrganizationService organizationService, IGoogleSecretManager secretManager)
        {
            _logger = logger;
            _organizationService = organizationService;
            _secretManager = secretManager;
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
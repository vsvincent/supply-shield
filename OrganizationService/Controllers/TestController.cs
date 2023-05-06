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
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok("Successful Request Woohoo");
        }
    }
}
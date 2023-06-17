using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Service;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string email)
        {
            IUser user = await _userService.GetUser(email);
            _logger.LogInformation("Getting user with email: " +  email);
            if (user == null)
            {
                return NoContent();
            }
            return Ok(await _userService.GetUser(email));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            return Ok(await _userService.SetUser(user));
        }
    }
}
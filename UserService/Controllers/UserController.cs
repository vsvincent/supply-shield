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

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] User user)
        {
            return Ok(await _userService.SetUser(user));
        }
    }
}
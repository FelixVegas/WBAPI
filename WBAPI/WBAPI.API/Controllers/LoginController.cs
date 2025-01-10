using Microsoft.AspNetCore.Mvc;
using WBAPI.API.Models;
using WBAPI.API.Services.AuthService;

namespace WBAPI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAuthService _authService;

        public LoginController(ILogger<LoginController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        /// <summary>
        /// Login with username and password to return an access token
        /// Credentials are mocked, just pass an empty body
        /// </summary>
        /// <param name="request">Login request, includes username, password and rememberMe</param>
        /// <returns></returns>
        [HttpPost(Name = "Login")]
        public IActionResult Login()
        {
            return Ok(_authService.Login("abc","123"));
        }

    }
}

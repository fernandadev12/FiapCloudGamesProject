using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private ILogger<AuthController> _logger;
        private IConfiguration _configuration;

        [HttpPost(Name = "autenticate")]
        [Authorize]
        public IActionResult Autenticate(string username, string password)
        {
            var token = GenerateTokenAuth(username, password);
            if (token == null)
            {
                _logger.LogError("Unable to generate token for user authorization");
                throw new UnauthorizedAccessException("Unable to generate token for user authorization");
            }
            else
            {
                _logger.LogInformation("Token generated for user authorization");
                return Ok(token);
            }
        }

        private string GenerateTokenAuth(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}

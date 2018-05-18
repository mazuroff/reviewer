using Microsoft.AspNetCore.Mvc;
using Reviewer.Core.Interfaces;
using Reviewer.Core.Models;

namespace Reviewer.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("token")]
        [ProducesResponseType(typeof(TokenModel), 200)]
        public IActionResult Login([FromBody] LoginCredentials loginCredentials)
        {
            var token = _tokenService.GetToken(loginCredentials);

            if (token != null)
            {
                return Ok(token);
            }
            return BadRequest();
        }
    }
}
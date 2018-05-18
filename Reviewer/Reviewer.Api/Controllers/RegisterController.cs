using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reviewer.Core.Interfaces;
using Reviewer.Core.Models;

namespace Reviewer.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/auth/register")]
    public class RegisterController : Controller
    {
        private readonly IRegistrationService registrationService;

        public RegisterController(IRegistrationService _registrationService)
        {
            registrationService = _registrationService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisteredUserModel), 200)]
        public IActionResult Register([FromBody] UserRegistrationModel userRegistration)
        {
            registrationService.Register(userRegistration);
            return Ok();
        }
    }
}
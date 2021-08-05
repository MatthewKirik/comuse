using AuthAPI.Models;
using AuthLogic.Services;
using DataTransfer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService AuthService { get; }
        public AuthController(IAuthService authService)
        {
            AuthService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserDTO>> RegisterUser(
            [FromBody] AuthorizationData data)
        {
            var registeredUser = await AuthService.RegisterUser(data.Email, data.Password);
            if (registeredUser == null)
                return Conflict("User with this email already exists.");
            return registeredUser;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> GetJWT(
            [FromBody] AuthorizationData data)
        {
            string jwt = await AuthService.GetJWT(data.Email, data.Password);
            if (jwt == null)
                return NotFound("Email or password is incorrect.");
            return jwt;
        }
    }
}

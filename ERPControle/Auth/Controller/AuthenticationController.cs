using Auth.DTO;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controller
{
    [Route("api/authentication/")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {   

                var token = await _authenticationService.LoginAsync(loginDto);

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { message = "Usuário ou senha inválidos" });
                }

                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

using DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Services.Authentication;

namespace Controller.Authentication
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
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var token = await _authenticationService.LoginAsync(loginDTO);

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
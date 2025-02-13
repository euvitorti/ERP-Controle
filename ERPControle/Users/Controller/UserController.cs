using Microsoft.AspNetCore.Mvc;
using Users.DTO;
using Users.Services;

namespace Users.Controller
{
    [ApiController]
    [Route("api/user/")]
    public class UserController : ControllerBase
    {
        private readonly IUserService iuserService;

        public UserController(IUserService iuserService)
        {
            this.iuserService = iuserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                await iuserService.RegisterAsync(registerDto);
                return Ok(new { message = "Usuário registrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

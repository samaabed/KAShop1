using KASHOP.BLL;
using KASHOP.DAL.DTO.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAShop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var (sucess, errors) = await _authService.RegisterAsync(request);

            return sucess ? Ok() : BadRequest(new {errors});
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var (sucess, errors, response) = await _authService.LoginAsync(request);

            return sucess ? Ok(response) : BadRequest(new { errors });
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var (success, error) = await _authService.ConfirmEmailAsync(userId, token);

            return success ? Ok() : BadRequest(new { error });
        }
    }
}

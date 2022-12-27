using diplomski.Data.Dtos;
using diplomski.Services.TokenService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diplomski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private ITokenService _tokenService;
        
        public AdminController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AdminInputDataDto adminInputData)
        {
            bool authenticated = await _tokenService.AuthenticateUser(adminInputData);
            if (!authenticated)
                return Unauthorized();

            string token = _tokenService.GenerateJwt(adminInputData);
            return StatusCode(StatusCodes.Status200OK, new {token = token });
        }
    }
}

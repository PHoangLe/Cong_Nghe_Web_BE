using BusinessObjects.DTO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;
using BusinessObjects.DTO.AuthDTO;

namespace MenuMinderAPI.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController( AuthService authService)
        {
            this._authService = authService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dataLoginInvo)
        {

            ApiResponse<ResultLoginDto> response = new ApiResponse<ResultLoginDto>();
            ResultLoginDto resultLogin = await this._authService.loginWithEmailPassword(dataLoginInvo);
            response.data = resultLogin;
            response.message = "login success.";

            return Ok(response);
        }
    }
}

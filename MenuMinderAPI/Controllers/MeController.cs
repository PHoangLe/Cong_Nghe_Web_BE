using BusinessObjects.DTO.AccountDTO;
using BusinessObjects.DTO.AuthDTO;
using BusinessObjects.DTO;
using BusinessObjects.Enum;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Exceptions;
using BusinessObjects.DataModels;

namespace MenuMinderAPI.Controllers
{

    [ApiController]
    [Route("/api/me")]

    public class MeController : Controller
    {
        private readonly MeService _meService;
        private readonly AccountService _accountService;

        public MeController(MeService meService, AccountService accountService)
        {
            this._meService = meService;
            this._accountService = accountService;
        }

        // GET: api/me/my-account
        [HttpGet("my-account")] 
        public async Task<ActionResult> GetMyAccount ()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            ApiResponse<Account> response = new ApiResponse<Account>();
            Account accountResult = await this._accountService.getAccountToUpdate(userFromToken.AccountId);
            response.data = accountResult;

            return Ok(response);
        }

        // PUT: api/me/update-password
        [HttpPut("update-password")]
        public async Task<ActionResult> CreateAccount([FromBody] UpdatePasswordDto dataInvo)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            ApiResponse<EmptyResult> response = new ApiResponse<EmptyResult>();
            await this._meService.updatePassword(userFromToken.AccountId, dataInvo);
            response.message = "Password has been updated.";

            return Ok(response);
        }

        // PUT: api/me/update
        [HttpPut("update")]
        public async Task<ActionResult> UpdateMyAccount([FromBody] UpdateAccountDto dataInvo)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            ApiResponse<EmptyResult> response = new ApiResponse<EmptyResult>();
            await this._accountService.UpdateAccount(userFromToken.AccountId, dataInvo);
            response.message = "Account has been updated.";

            return Ok(response);
        }
    }
}

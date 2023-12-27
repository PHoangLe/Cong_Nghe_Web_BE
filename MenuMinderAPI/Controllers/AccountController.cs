using BusinessObjects.DTO.AuthDTO;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using Services;
using BusinessObjects.DTO.AccountDTO;
using MenuMinderAPI.MiddleWares;
using System.Security.Claims;
using BusinessObjects.Enum;
using Services.Exceptions;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.PermitDTO;

namespace MenuMinderAPI.Controllers
{
    [ApiController]
    [Route("/api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController (AccountService accountService)
        {
            this._accountService = accountService;
        }

        // POST: api/accounts/create
        [HttpPost("create")]
        public async Task<ActionResult> CreateAccount([FromBody] CreateAccountDto dataInvo)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            // Check ROLE
            if(userFromToken.Role != EnumRole.ADMIN.ToString())
            {
                throw new UnauthorizedException("Only ADMIN have permission to access this resource.");
            }

            ApiResponse<ResultAccountDTO> response = new ApiResponse<ResultAccountDTO>();
            ResultAccountDTO resultAccount = await this._accountService.createAccount(dataInvo);
            response.data = resultAccount;
            response.message = "create account success.";

            return Ok(response);
        }

        // GET: api/accounts/all
        [HttpGet("all")]
        public async Task<ActionResult> GetAllAccount([FromQuery] string? search = "")
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            // Check ROLE
            if (userFromToken.Role != EnumRole.ADMIN.ToString())
            {
                throw new UnauthorizedException("Only ADMIN have permission to access this resource.");
            }

            ApiResponse<List<AccountSuccinctDto>> response = new ApiResponse<List<AccountSuccinctDto>>();
            List<AccountSuccinctDto> resultAccount = await this._accountService.getListStaffAccount(search);
            response.data = resultAccount;
            //response.message = "get all success.";

            return Ok(response);
        }

        // GET: api/accounts/:accountId
        [HttpGet("{accountId}")]
        public async Task<ActionResult> GetDetailAccount(string accountId)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            // Check ROLE
            if (userFromToken.Role != EnumRole.ADMIN.ToString())
            {
                throw new UnauthorizedException("Only ADMIN have permission to access this resource.");
            }

            ApiResponse<ResultAccountDTO> response = new ApiResponse<ResultAccountDTO>();
            ResultAccountDTO resultAccount = await this._accountService.getDetailAccount(accountId);
            response.data = resultAccount;

            return Ok(response);
        }

        // PUT: api/accounts/update/:accountId
        [HttpPut("{accountId}")]
        public async Task<ActionResult> UpdateAccount(string accountId, [FromBody] UpdateAccountDto accountInvo)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            // Check ROLE
            if (userFromToken.Role != EnumRole.ADMIN.ToString())
            {
                throw new UnauthorizedException("Only ADMIN have permission to access this resource.");
            }

            ApiResponse<ResultAccountDTO> response = new ApiResponse<ResultAccountDTO>();
            await this._accountService.UpdateAccount(accountId, accountInvo);
            response.message = "update account success";

            return Ok(response);
        }

        [HttpPut("permits/{accountId}")]
        public async Task<ActionResult> UpdatePermits(string AccountId, [FromBody] UpdatePermitAccountDto dataInvo)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            // Check ROLE
            if (userFromToken.Role != EnumRole.ADMIN.ToString())
            {
                throw new UnauthorizedException("Only ADMIN have permission to access this resource.");
            }

            ApiResponse<string> response = new ApiResponse<string>();
            await this._accountService.updateAccountPermits(AccountId, dataInvo.permissionIds);
            response.message = "update permission for account success";

            return Ok(response);
        }

        [HttpDelete("block/{accountId}")]
        public async Task<ActionResult> BlockAccount(string accountId, [FromQuery] BlockAccountDto dataInvo)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            // Check ROLE
            if (userFromToken.Role != EnumRole.ADMIN.ToString())
            {
                throw new UnauthorizedException("Only ADMIN have permission to access this resource.");
            }

            ApiResponse<string> response = new ApiResponse<string>();
            await this._accountService.blockAccount(accountId, dataInvo.isBlock);
            response.message = "Account has been locked!";

            return Ok(response);
        }

        [HttpDelete("delete/{accountId}")]
        public async Task<ActionResult> DeleteBlockAccount(string accountId)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            // Check ROLE
            if (userFromToken.Role != EnumRole.ADMIN.ToString())
            {
                throw new UnauthorizedException("Only ADMIN have permission to access this resource.");
            }

            ApiResponse<string> response = new ApiResponse<string>();
            await this._accountService.deleteAccount(accountId);
            response.message = "Account has been Deleted!";

            return Ok(response);
        }
    }
}

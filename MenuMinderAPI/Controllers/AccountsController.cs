using BusinessObjects.DTO;
using BusinessObjects.DTO.AccountDTO;
using BusinessObjects.DTO.CategoryDTO;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MenuMinderAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(AccountService accountService, ILogger<AccountsController> logger)
        {
            this._accountService = accountService;
            this._logger = logger;
        }

        // GET: api/accounts
        [HttpGet]
        public async Task<ActionResult> GetAllAccounts()
        {
            ApiResponse<List<ResultAccountDTO>> response = new ApiResponse<List<ResultAccountDTO>>();

            try
            {
                List<ResultAccountDTO> results = await this._accountService.GetAllAccounts();
                response.data = results;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }

        // GET api/<AccountsController>/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult> Get(string email)
        {
            ApiResponse<ResultAccountDTO> response = new ApiResponse<ResultAccountDTO>();

            try
            {
                ResultAccountDTO results = await this._accountService.GetAccountByEmail(email);
                response.data = results;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }

        // POST api/<AccountsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateAccountDTO dataInvo)
        {
            ApiResponse<String> response = new ApiResponse<String>();

            try
            {
                await this._accountService.CreateNewAccount(dataInvo);
                response.data = "create account success";
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }

        // PUT api/<AccountsController>/5
        [HttpPut("{email}")]
        public async Task<ActionResult> Put(string email, [FromBody] CreateAccountDTO dataInvo)
        {
            ApiResponse<String> response = new ApiResponse<String>();

            try
            {
                await this._accountService.UpdateAccount(email, dataInvo);
                response.data = "update account success";
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }

        // DELETE api/<AccountsController>/{email}
        [HttpDelete("{email}")]
        public async Task<ActionResult> Delete(string email)
        {
            ApiResponse<String> response = new ApiResponse<String>();

            try
            {
                await this._accountService.DeleteAccount(email);
                response.data = "lock account success";
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }
    }
}

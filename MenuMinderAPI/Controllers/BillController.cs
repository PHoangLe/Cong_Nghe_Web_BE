using BusinessObjects.DTO.AuthDTO;
using BusinessObjects.DTO.ServingDTO;
using BusinessObjects.DTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Services;
using BusinessObjects.DTO.BillDTO;

namespace MenuMinderAPI.Controllers
{
    [ApiController]
    [Route("/api/bills")]
    public class BillController : ControllerBase
    {
        private readonly BillService _billService;

        public BillController(BillService billService)
        {
            this._billService = billService;
        }

        // POST: api/bill/create
        [HttpPost("create")]
        public async Task<ActionResult> createServing([FromBody] BillCreateDto dataInvo)
        {
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            dataInvo.CreatedBy = Guid.Parse(userFromToken.AccountId);

            ApiResponse<NoContentResult> response = new ApiResponse<NoContentResult>();
            await this._billService.createBill(dataInvo);
            response.message = "Created success";

            return Ok(response);
        }
    }
}
